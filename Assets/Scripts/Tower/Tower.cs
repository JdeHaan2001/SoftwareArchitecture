using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [field: SerializeField]
    public virtual int Damage { get; set; } = 5;
    [field: SerializeField]
    public virtual int BuyCost { get; set; } = 10;
    [field: SerializeField]
    public virtual int UpgradeCost { get; set; } = 15;
    [field: SerializeField]
    public virtual float Range { get; set; } = 50f;
    [field: SerializeField]
    protected virtual float UpgradeMultiplier { get; set; } = 1.3f;
    [field: SerializeField]
    protected float RateOfFire { get; set; } = 1.5f;


    protected Enemy target;
    protected bool isShooting = false;

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

    public virtual void DetectEnemyInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Range);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform.tag == "Enemy")
            {
                Enemy enemy = hitCollider.GetComponent<Enemy>();

                if (enemy == null)
                    Debug.Log("Object doesn't contain Enemy component", this);
                else
                    target = enemy;
            }
        }

        if (target != null && !isShooting)
        {
            IEnumerator coroutine = Shoot(target);
            StartCoroutine(coroutine);
        }

    }

    public virtual IEnumerator Shoot(Enemy pEnemy)
    {
        yield return new WaitForSeconds(0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
