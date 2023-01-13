using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IEnemy
{
    int Health { get; set; }
    int Damage { get; set; }
    int MoneyDropped { get; set; }

    void spawn(Transform pSpawnPos);
    void move();
    void handleDeath();
    void getDamage();
    void dealDamage();
}
