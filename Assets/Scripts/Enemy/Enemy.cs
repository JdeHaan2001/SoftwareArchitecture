using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    private List<GameObject> wayPoints;

    private float health;
    private float speed;
    private int money;
    private int damage;

    public Action<Enemy, int> OnEnemyDeath;
    public Action<int> OnEnemyDamage;

    public void SetData(EnemySO pEnemySO)
    {
        health = pEnemySO.Health;
        speed = pEnemySO.Speed;
        money = pEnemySO.Money;
        damage = pEnemySO.Damage;
        wayPoints = pEnemySO.wayPoints;
    }

    public int GetDamage() => damage;

    public void DealDamageToEnemy(int pDamage)
    {
        health -= pDamage;
        OnEnemyDamage?.Invoke(pDamage);
        if (health <= 0)
            handleDeath();
        Debug.Log("Dealt damage to enemy");
    }

    private void handleDeath()
    {
        Destroy(this.gameObject);

        OnEnemyDeath?.Invoke(this, money);
    }

    private void Update()
    {
        Vector3 deltaVec = wayPoints[0].transform.position - this.transform.position;

        Vector3 moveDirection = new Vector3(0, 0, 1);

        this.transform.position += deltaVec.normalized * speed * Time.deltaTime;
    }

    public float GetHealth() => health;
}
