using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    private float health;
    private float speed;
    private int money;
    private int damage;

    public Action<int> onEnemyDeath;

    public void SetData(EnemySO pEnemySO)
    {
        health = pEnemySO.Health;
        speed = pEnemySO.Speed;
        money = pEnemySO.Money;
        damage = pEnemySO.Damage;
    }

    public int GetDamage() => damage;

    public void DealDamageToEnemy(int pDamage)
    {
        health -= pDamage;

        if (health <= 0)
            handleDeath();
        Debug.Log("Dealt damage to enemy");
    }

    private void handleDeath()
    {
        Destroy(this.gameObject);

        onEnemyDeath?.Invoke(money);
    }

    private void Update()
    {
        Vector3 moveDirection = new Vector3(0, 0, 1);

        this.transform.position += moveDirection * speed * Time.deltaTime;
    }
}
