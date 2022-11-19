using UnityEngine;

public class GameManager : MonoBehaviour
{
    private EnemyManager enemyManager;
    private UIManager uiManager;
    private TowerManager towerManager;
    private SelectionManager selectionManager;

    public enum GameState
    {
        Preparation,
        Battle
    }

    [HideInInspector]
    public GameState gameState = GameState.Preparation;

    private int waveNumber = 1;

    private void Start()
    {
        enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
        uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        towerManager = GameObject.FindWithTag("TowerManager").GetComponent<TowerManager>();
        selectionManager = GameObject.FindWithTag("SelectionManager").GetComponent<SelectionManager>();
    }

    public void StartBattle()
    {
        gameState = GameState.Battle;
        StartCoroutine(enemyManager.SpawnEnemies());
    }

    public void EndBattle()
    {
        gameState = GameState.Preparation;
        uiManager.EndBattle();
        ++waveNumber;
        uiManager.SetWaveNumber(waveNumber);
    }

    public void EndGame()
    {
        enemyManager.StopEnemies();
        towerManager.StopTowers();
        Destroy(selectionManager);
        uiManager.ShowGameOverWindow(waveNumber - 1);
    }
}
