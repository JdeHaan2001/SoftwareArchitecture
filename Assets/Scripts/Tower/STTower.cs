using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STTower : Tower
{
    public float RateOfFire = 1.5f;

    private Enemy target;
    private bool isShooting = false;

    private void Update()
    {
        detectEnemyInRange();
    }

    public override int BuyTower(Vector3 pPosition)
    {
        return base.BuyTower(pPosition);
    }

    public override int Upgrade()
    {
        return base.Upgrade();
    }

    private void detectEnemyInRange()
    {
        #region sphereCast

        //RaycastHit hit;
        ////TODO: This ray isn't correct. Need to find a way to center the spherecast on the tower itself
        //Ray ray = new Ray(transform.position, transform.up * -1);
        //if (Physics.SphereCast(ray, base.Range, out hit, 1))
        //{
        //    if (hit.transform.tag == "Enemy" && target == null)
        //    {
        //        target = hit.transform.GetComponent<Enemy>();
        //        Debug.Log("Enemy in Range");
        //    }
        //}
        //else
        //    target = null;

        #endregion

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, base.Range);
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
            IEnumerator coroutine = shoot(target);
            StartCoroutine(coroutine);
        }
        
    }

    private IEnumerator shoot(Enemy pEnemy)
    {
        isShooting = true;
        while (target != null)
        {
            Debug.Log("Shooting");
            target.DealDamageToEnemy(base.Damage);
            yield return new WaitForSeconds(RateOfFire);
        }
        isShooting = false;
    }
}
