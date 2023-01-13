using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int Damage = 5;
    public int BuyCost = 10;
    public int UpgradeCost = 15;
    public float UpgradeMultiplier = 1.3f;
    public float Range = 50f;

    public virtual int BuyTower(Vector3 pPosition)
    {
        Instantiate(gameObject, pPosition, Quaternion.identity);
        //Implement buying a tower
        //Maybe spawning the tower should be a different function?

        return BuyCost;
    }

    public virtual int Upgrade()
    {
        //implement tower upgrade
        int upgradeCost = UpgradeCost;

        UpgradeCost = (int)Math.Round(UpgradeCost * UpgradeMultiplier, 0);
        Debug.Log("new upgrade cost " + UpgradeCost);
        return upgradeCost;
    }

    public virtual int DealDamage()
    {
        return Damage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
