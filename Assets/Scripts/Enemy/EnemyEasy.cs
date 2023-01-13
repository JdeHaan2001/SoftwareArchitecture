using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEasy : MonoBehaviour, IEnemy
{
    private int health;
    private int damage;
    private int moneyDropped;
    
    public int Health { get => health; set => health = value; }
    public int Damage { get => damage; set => damage = value; }
    public int MoneyDropped { get => moneyDropped; set => moneyDropped = value; }

    public EnemyEasy()
    {

    }

    private void OnEnable()
    {
        
    }

    public void dealDamage()
    {
        throw new System.NotImplementedException();
    }

    public void getDamage()
    {
        throw new System.NotImplementedException();
    }

    public void handleDeath()
    {
        throw new System.NotImplementedException();
    }

    public void move()
    {
        throw new System.NotImplementedException();
    }

    public void spawn(Transform pSpawnPos)
    {
        throw new System.NotImplementedException();
    }
}
