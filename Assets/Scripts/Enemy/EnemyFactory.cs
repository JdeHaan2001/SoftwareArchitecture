using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField]
    private List<Transform> wayPoints = new List<Transform>();

    public GameObject Enemy;
    public Transform SpawnPos;

    /// <summary>
    /// Will spawn the given enemy type, used for debugging and testing purposes
    /// </summary>
    /// <param name="pEnemyType">Index for the enemy Types list</param>
    public Enemy SpawnEnemyTest(EnemySO pEnemy)
    {
        //Error prevention I guess
        if (pEnemy == null) Debug.LogError("Given paramater is NULL", this);

        //Instantiate(enemyTypes[pEnemyType].enemyVisual, SpawnPos.position, SpawnPos.rotation);
        return pEnemy.Spawn(SpawnPos.position, SpawnPos.rotation, wayPoints);
    }

    /// <summary>
    /// Will span a random enemy from the given list
    /// </summary>
    /// <param name="pEnemies"></param>
    /// <returns></returns>
    public Enemy SpawnEnemy(List<EnemySO> pEnemies)
    {
        //if (pEnemies == null) Debug.LogError("Given List is NULL", this);
        if (pEnemies.Count <= 0) Debug.LogError("Given list doesn't contain any enemies", this);

        int randomNumber = Random.Range(0, pEnemies.Count);

        return pEnemies[randomNumber].Spawn(SpawnPos.position, SpawnPos.rotation, wayPoints);
    }
}
