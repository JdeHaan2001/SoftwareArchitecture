using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STTower : Tower
{
    private void Update()
    {
        DetectEnemyInRange();
    }

    public override int BuyTower(Vector3 pPosition)
    {
        return base.BuyTower(pPosition);
    }

    public override int Upgrade()
    {
        return base.Upgrade();
    }

    //public override void DetectEnemyInRange()
    //{
    //    Collider[] hitColliders = Physics.OverlapSphere(transform.position, base.Range);
    //    foreach (var hitCollider in hitColliders)
    //    {
    //        if (hitCollider.transform.tag == "Enemy")
    //        {
    //            Enemy enemy = hitCollider.GetComponent<Enemy>();

    //            if (enemy == null)
    //                Debug.Log("Object doesn't contain Enemy component", this);
    //            else
    //                base.target = enemy;
    //        }
    //    }

    //    if (target != null && !isShooting)
    //    {
    //        IEnumerator coroutine = shoot(target);
    //        StartCoroutine(coroutine);
    //    }
        
    //}

    public override IEnumerator Shoot(Enemy pEnemy)
    {
        base.isShooting = true;
        while (target != null)
        {
            Debug.Log("Shooting");
            target.DealDamageToEnemy(base.Damage);
            yield return new WaitForSeconds(base.RateOfFire);
        }
        base.isShooting = false;
    }
}
