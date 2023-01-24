using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [Tooltip("Sets the amount of waves\nIn final version needs to have a minimum value of 5")]
    [SerializeField] [Range(1, 10)] private int amountOfWaves = 1;
    [Tooltip("Amount of seconds for when the next enemy spawns")]
    [SerializeField] [Range(0.5f, 5f)] private float enemyDelay = 2f;
    [Tooltip("Maximum amount of enemies in a wave")]
    [SerializeField] [Range(1, 50)] private int maxEnemies = 10;
    [Tooltip("Time, in seconds, the player has between waves to buy/repair/upgrade towers")]
    [SerializeField] [Range(15f, 90f)] private float timeBetweenWaves = 30f;
    [SerializeField] private BaseManager baseManager;

    private EnemyFactory enemyFactory = null;
    private UIManager uiManager = null;
    private int waveNumber = 0;
    private bool isGamePaused = false;

    private List<Enemy> activeEnemies = new List<Enemy>();

    private void Start()
    {

        enemyFactory = this.GetComponent<EnemyFactory>();
        uiManager = this.GetComponent<UIManager>();

        #region NULL checks
        if (enemyFactory == null) Debug.LogError("Variable enemyFactory is NULL", this);
        if (uiManager == null) Debug.LogError("Variable waveManager is NULL", this);
        #endregion
        
        startWave();
    }

    private void OnEnable()
    {
        baseManager.OnLoseGame += pauseGame;
    }

    private void OnDisable()
    {
        baseManager.OnLoseGame -= pauseGame;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
            enemyFactory.SpawnEnemy();
#endif
    }

    private void pauseGame()
    {
        isGamePaused = true;
        Debug.Log("Pausing game");
    }

    private void startWave()
    {
        waveNumber++;
        uiManager.UpdateWaveNumberText(waveNumber);
        StartCoroutine("spawnEnemies");
    }

    private IEnumerator spawnEnemies()
    {
        int spawnedEnemies = 0;

        while (spawnedEnemies < maxEnemies && !isGamePaused)
        {
            Debug.Log("Is game paused: " + isGamePaused);
            yield return new WaitForSeconds(enemyDelay);
            Enemy enemy = enemyFactory.SpawnEnemy();
            activeEnemies.Add(enemy);
            enemy.OnEnemyDeath += handleEnemyDeath;

            spawnedEnemies++;
        }

        if (spawnedEnemies == maxEnemies && waveNumber != amountOfWaves)
            StartCoroutine("timeToBuild");
    }

    private void handleEnemyDeath(Enemy pEnemy, int pMoney)
    {
        pEnemy.OnEnemyDeath -= handleEnemyDeath;
        activeEnemies.Remove(pEnemy);
        MoneyManager.Instance.AddMoney(pMoney);
    }

    private IEnumerator timeToBuild()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
    }
}
