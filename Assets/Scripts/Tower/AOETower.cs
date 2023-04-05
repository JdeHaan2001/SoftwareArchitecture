using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Are of attack tower type. This tower will damage all enemies that are within its range.
/// </summary>
public class AOETower : Tower
{
    [SerializeField]
    private Material attackMaterial;
    [SerializeField][Range(0.1f, 5.0f)]
    private float attackIndicationDuration = 0.2f;

    private List<Enemy> enemies = new List<Enemy>();
    private Material originalMaterial;

    private void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
    }

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
            StartCoroutine("attackIndication");
            foreach (Enemy enemy in enemies)
                enemy.DealDamageToEnemy(base.Damage);
        }

        yield return new WaitForSeconds(base.RateOfFire);
        base.isShooting = false;
    }

    private IEnumerator attackIndication()
    {
        GetComponent<Renderer>().material = attackMaterial;
        yield return new WaitForSeconds(attackIndicationDuration);
        GetComponent<Renderer>().material = originalMaterial;
    }}
