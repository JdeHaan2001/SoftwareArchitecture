using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AOETower : Tower
{
    private List<Enemy> enemies = new List<Enemy>();

    private void Update()
    {
        DetectEnemyInRange();
    }

    public override void DetectEnemyInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Range);
        List<Enemy> tempEnemies = new List<Enemy>();
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform.tag == "Enemy")
            {
                Enemy enemy = hitCollider.GetComponent<Enemy>();

                if (enemy == null)
                    Debug.Log("Object doesn't contain Enemy component", this);
                else
                    tempEnemies.Add(enemy);
            }
        }

        if (tempEnemies.Count > 0 && !base.isShooting)
        {
            enemies = new List<Enemy>(tempEnemies);
            IEnumerator coroutine = Shoot(target);
            StartCoroutine(coroutine);
        }
    }

    public override IEnumerator Shoot(Enemy pEnemy)
    {
        base.isShooting = true;

        if (enemies.Count > 0)
        {
            foreach (Enemy enemy in enemies)
                enemy.DealDamageToEnemy(base.Damage);
        }

        yield return new WaitForSeconds(base.RateOfFire);
        base.isShooting = false;
    }
}
