using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    [Tooltip("Time, in seconds, the player has between waves to buy/repair/upgrade towers")]
    [SerializeField] [Range(15f, 90f)] private float timeBetweenWaves = 30f;
    [SerializeField] private BaseManager baseManager;
    [SerializeField]
    private string winSceneName;
    [SerializeField]
    private List<WaveSO> waves = new List<WaveSO>();

    private EnemyFactory enemyFactory = null;
    private UIManager uiManager = null;
    private int waveNumber = 0;
    private int spawnedEnemyIndex = 0;
    private bool isGamePaused = false;

    private List<Enemy> activeEnemies = new List<Enemy>();
    public bool IsWaveActive { get; private set; } = true; // is set to TRUE to "pause" the game.
                                                           // If it's set to FALSE, the payer can buy towers before the game has started

    public System.Action<bool> OnIsWaveActiveChange;
    public System.Action<int> OnBuildTimeChange;
    public System.Action OnWaveStart;

    private void Start()
    {
        enemyFactory = this.GetComponent<EnemyFactory>();
        uiManager = this.GetComponent<UIManager>();
        #region NULL checks
        if (enemyFactory == null) Debug.LogError("Variable enemyFactory is NULL", this);
        if (uiManager == null) Debug.LogError("Variable waveManager is NULL", this);
        #endregion
        //startWave(waves[0]);
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
            enemyFactory.SpawnEnemyTest(waves[0].enemies[0]);
#endif
    }

    private void pauseGame()
    {
        isGamePaused = true;
        Debug.Log("Pausing game");
    }

    private void startWave(WaveSO pWave)
    {
        IsWaveActive = true;
        OnWaveStart?.Invoke();
        OnIsWaveActiveChange?.Invoke(true);
        spawnedEnemyIndex = 0;
        waveNumber++;
        uiManager.UpdateWaveNumberText(waveNumber);

        StartCoroutine(spawnEnemies(pWave));
    }

    private IEnumerator spawnEnemies(WaveSO pWave)
    {
        int spawnedEnemies = 0;

        while (spawnedEnemies < pWave.MaxEnemies && !isGamePaused)
        {
            yield return new WaitForSeconds(pWave.EnemyDelay);
            Enemy enemy = enemyFactory.SpawnEnemy(pWave.enemies);
            activeEnemies.Add(enemy);
            spawnedEnemyIndex++;
            enemy.OnEnemyDeath += handleEnemyDeath;

            spawnedEnemies++;
        }

        if (spawnedEnemies == pWave.MaxEnemies && activeEnemies.Count == 0)
        {
            Debug.Log("End of wave " + waveNumber);
            handleEndOfWave();
        }

        //if (spawnedEnemies == pWave.MaxEnemies && waveNumber != waves.Count)
        //    StartCoroutine("timeToBuild");
    }

    private void handleEnemyDeath(Enemy pEnemy, int pMoney)
    {
        pEnemy.OnEnemyDeath -= handleEnemyDeath;
        activeEnemies.Remove(pEnemy);
        MoneyManager.Instance.AddMoney(pMoney);

        handleEndOfWave();
    }

    /// <summary>
    /// Handles the time between waves to build and upgrade towers
    /// </summary>
    /// <returns></returns>
    private IEnumerator timeToBuild()
    {
        Debug.Log("Time to build has started");
        IsWaveActive = false;

        OnIsWaveActiveChange?.Invoke(IsWaveActive);
        OnBuildTimeChange?.Invoke((int)timeBetweenWaves);

        int timeIndex = 0;
        while (timeIndex < timeBetweenWaves)
        {
            //Waits for 1 second
            yield return new WaitForSeconds(1);
            timeIndex++;
            OnBuildTimeChange?.Invoke((int)timeBetweenWaves - timeIndex);
        }

        startWave(waves[waveNumber]);
    }

    public void handleEndOfWave()
    {
        if (activeEnemies.Count == 0 && spawnedEnemyIndex == waves[waveNumber - 1].MaxEnemies)
        {
            Debug.Log("End of wave " + waveNumber);
            if (waveNumber >= waves.Count)
            {
                Debug.Log("Player won");
                SceneManager.LoadScene(winSceneName);
            }

            StartCoroutine("timeToBuild");
        }
    }



    /// <summary>
    /// This function is only intended to be called by events. E.g. Button OnClick event
    /// </summary>
    public void StartTimeToBuild()
    {
        StartCoroutine("timeToBuild");
    }
}
