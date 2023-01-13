using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField]
    private List<EnemySO> enemyTypes = new List<EnemySO>();

    public GameObject Enemy;
    public Transform SpawnPos;

    /// <summary>
    /// Will spawn the given enemy type
    /// </summary>
    /// <param name="pEnemyType">Index for the enemy Types list</param>
    public void SpawnEnemy(int pEnemyType=0)
    {
        //Error prevention I guess
        if (pEnemyType > enemyTypes.Count) Debug.LogError("Enemy Type not found, index is bigger than amount of enemy types", this);

        //Instantiate(enemyTypes[pEnemyType].enemyVisual, SpawnPos.position, SpawnPos.rotation);
        enemyTypes[pEnemyType].Spawn(SpawnPos.position, SpawnPos.rotation);
    }

    /// <summary>
    /// Will spawn a random enemy
    /// </summary>
    public void SpawnRandomEnemy()
    {

    }
}
