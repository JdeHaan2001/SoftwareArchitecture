using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Single Target tower. This tower will attack the closest enemy within it's range.
/// </summary>
public class STTower : Tower
{
    [SerializeField]
    private GameObject muzzleFlash;
    [SerializeField][Range(0.1f, 5.0f)]
    private float muzzleFlashDuration = 0.2f;

    private void Start()
    {
        muzzleFlash.SetActive(false);
    }

    private void Update()
    {
        DetectEnemyInRange();
        aimAtTarget();
    }

    private void aimAtTarget()
    {
        if (base.target == null) return;

        Vector3 deltaVec = base.target.transform.position - this.transform.position;
        deltaVec.y = 0;
        Quaternion rotation = Quaternion.LookRotation(deltaVec);
        this.transform.rotation = rotation;
    }

    private IEnumerator handleMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(muzzleFlashDuration);
        muzzleFlash.SetActive(false);
    }

    public override IEnumerator Shoot(Enemy pEnemy)
    {
        base.isShooting = true;
        while (target != null)
        {
            StartCoroutine("handleMuzzleFlash");
            Debug.Log("Shooting");
            target.DealDamageToEnemy(base.Damage);
            yield return new WaitForSeconds(base.RateOfFire);
        }
        base.isShooting = false;
    }
}
