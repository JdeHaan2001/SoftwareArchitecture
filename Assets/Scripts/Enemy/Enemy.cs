using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum EnemyState
{
    NORMAL_STATE = 0, SLOW_STATE, LOWER_DAMAGE_STATE, EXTRA_MONEY_STATE
}

/// <summary>
/// This class handles all the logic for the enemies. E.g. movement, what happens upon death or what happens when the enemy gets a debuff.
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Light attackIndicationLight;
    [SerializeField]
    [Range(0.1f, 5.0f)]
    private float attackIndicationDuration = 0.2f;
    [SerializeField]
    EnemyState currenState = EnemyState.NORMAL_STATE;

    private List<Transform> wayPoints;

    private float health;
    private float speed;
    private int money;
    private int damage;

    private int wayPointIndex = 0;

    public event Action<Enemy, int> OnEnemyDeath;
    public event Action<int> OnEnemyDamage;

    private void Start()
    {
        attackIndicationLight.gameObject.SetActive(false);
    }

    /// <summary>
    /// Sets all the data in the enemy class.
    /// </summary>
    /// <param name="pEnemySO">Scriptable object where the data gets read from</param>
    public void SetData(EnemySO pEnemySO)
    {
        health = pEnemySO.Health;
        speed = pEnemySO.Speed;
        money = pEnemySO.Money;
        damage = pEnemySO.Damage;
    }

    /// <summary>
    /// Sets the waypoints for the waypoint based pathfinding algorithm
    /// </summary>
    /// <param name="pWayPoints"></param>
    public void SetWayPoints(List<Transform> pWayPoints) => wayPoints = pWayPoints;

    public int GetDamage() => damage;

    /// <summary>
    /// Deals damage to this enemy instance
    /// </summary>
    /// <param name="pDamage"></param>
    public void DealDamageToEnemy(int pDamage)
    {
        health -= pDamage;
        OnEnemyDamage?.Invoke(pDamage);
        if (health <= 0)
            handleDeath();
        Debug.Log("Dealt damage to enemy");
    }

    /// <summary>
    /// Debuffs the enemy based on the given debuff type paramater
    /// </summary>
    public void DebuffEnemy(debuffType pType, float pMultiplier)
    {
        if (currenState != EnemyState.NORMAL_STATE) return;

        switch (pType)
        {
            case debuffType.DAMAGE:
                currenState = EnemyState.LOWER_DAMAGE_STATE;
                damage = (int)Math.Round(damage / pMultiplier, MidpointRounding.AwayFromZero);
                Debug.Log("Enemy damage debuff");
                break;
            case debuffType.MONEY:
                currenState = EnemyState.EXTRA_MONEY_STATE;
                money = (int)Math.Round(money * pMultiplier, MidpointRounding.AwayFromZero);
                Debug.Log("Enemy money debuff");
                break;
            case debuffType.SPEED:
                currenState = EnemyState.SLOW_STATE;
                Debug.Log("Enemy speed debuff");
                speed /= pMultiplier;
                break;
        }

        StartCoroutine("debuffIndication");
    }
    public float GetHealth() => health;

    /// <summary>
    /// Removes the enemy from the game without adding money to the player
    /// Mainly used for when the enemy reaches the endpoint/base
    /// </summary>
    public void RemoveEnemyFromGame()
    {
        OnEnemyDeath?.Invoke(this, 0);
        Destroy(gameObject);
    }

    private void handleDeath()
    {
        Destroy(this.gameObject);

        OnEnemyDeath?.Invoke(this, money);
    }

    private void Update()
    {
        Vector3 deltaVec = wayPoints[wayPointIndex].transform.position - this.transform.position;

        if (deltaVec.magnitude <= 0.1)
            wayPointIndex++;

        this.transform.position += deltaVec.normalized * speed * Time.deltaTime;
    }

    /// <summary>
    /// Gives an indication that the player is debuffed. You can implement different types of indications per enemy state
    /// </summary>
    private IEnumerator debuffIndication()
    {
        //base functionality to make different events happen based on the state of the enemy
        //right now only the color of the light changes based on the enemy's state
        switch (currenState)
        {
            case EnemyState.LOWER_DAMAGE_STATE:
                attackIndicationLight.color = Color.red;
                break;
            case EnemyState.EXTRA_MONEY_STATE:
                attackIndicationLight.color = Color.yellow;
                break;
            case EnemyState.SLOW_STATE:
                attackIndicationLight.color = Color.cyan;
                break;
        }

        attackIndicationLight.gameObject.SetActive(true);
        yield return new WaitForSeconds(attackIndicationDuration);
        attackIndicationLight.gameObject.SetActive(false);
    }
}
