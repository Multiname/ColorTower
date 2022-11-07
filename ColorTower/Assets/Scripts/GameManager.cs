using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Preparation,
        Battle
    }

    public GameState gameState = GameState.Preparation;

    private EnemyManager enemyManager;
    private UIManager uiManager;
    private TowerManager towerManager;
    private SelectionManager selectionManager;
    private int enemyNumber;
    private int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
        uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        towerManager = GameObject.FindWithTag("TowerManager").GetComponent<TowerManager>();
        selectionManager = GameObject.FindWithTag("SelectionManager").GetComponent<SelectionManager>();

        ReloadEnemyNumber();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBattle()
    {
        gameState = GameState.Battle;
        StartCoroutine(enemyManager.SpawnEnemies());
    }

    private void EndBattle()
    {
        gameState = GameState.Preparation;
        uiManager.EndBattle();
        ReloadEnemyNumber();
        ++waveNumber;
        uiManager.SetWaveNumber(waveNumber);
    }

    public void DecrementEnemyNumber()
    {
        --enemyNumber;
        uiManager.SetEnemyNumber(enemyNumber);
        if (enemyNumber <= 0)
            EndBattle();
    }

    private void ReloadEnemyNumber()
    {
        enemyNumber = enemyManager.GenerateWave();
        uiManager.SetEnemyNumber(enemyNumber);
        uiManager.SetEnemyGroups();
    }

    public void EndGame()
    {
        enemyManager.StopEnemies();
        towerManager.StopTowers();
        Destroy(selectionManager);
        uiManager.ShowGameOverWindow(waveNumber - 1);
    }
}
