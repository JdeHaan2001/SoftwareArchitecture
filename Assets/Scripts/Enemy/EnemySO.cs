using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object to create a new enemy type.
/// </summary>
[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemySO : ScriptableObject
{
    public float Health = 10f;
    public float Speed = 5f;
    public int Money = 5;
    public int Damage = 5;
    public Enemy EnemyScript;

    public Enemy Spawn(Vector3 pPosition, Quaternion pRotation, List<Transform> pWayPoints)
    {
        GameObject enemyObj = Instantiate(EnemyScript.gameObject, pPosition, Quaternion.identity);
        Enemy enemy = enemyObj.GetComponent<Enemy>();
        enemy.SetData(this);
        enemy.SetWayPoints(pWayPoints);
        return EnemyScript;
    }
}
