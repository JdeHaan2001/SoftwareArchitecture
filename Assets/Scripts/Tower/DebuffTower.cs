using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum debuffType
{
    DAMAGE = 0, SPEED, MONEY
}

/// <summary>
/// Debuff tower type. This tower will debuff an enemy based on what debuff type is selected. You can also select the option to let the tower randomly
/// decide what type of debuff will be given to the enemy
/// </summary>
public class DebuffTower : Tower
{
    [SerializeField]
    private debuffType typeDebuff = debuffType.SPEED;
    [SerializeField]
    private float debuffMultiplier = 2f;
    [SerializeField] //If set to true, the tower will pick a random debuff type for the enemies
    private bool doesRandomDebuff = false;

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
            Debug.Log("debuffing enemies");
            foreach (Enemy enemy in enemies)
            {
                if (!doesRandomDebuff)
                    enemy.DebuffEnemy(typeDebuff, debuffMultiplier);
                else
                {
                    int randIndex = Random.Range(0, System.Enum.GetNames(typeof(debuffType)).Length);
                    enemy.DebuffEnemy((debuffType)randIndex, debuffMultiplier);
                }
            }
        }

        yield return new WaitForSeconds(base.RateOfFire);
        base.isShooting = false;
    }
}
