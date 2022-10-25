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
    private int enemyNumber;

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
        uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();

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

    public void DecrementEnemyNumber()
    {
        --enemyNumber;
        uiManager.SetEnemyNumber(enemyNumber);
        if (enemyNumber <= 0)
        {
            gameState = GameState.Preparation;
            uiManager.EndBattle();
            ReloadEnemyNumber();
        }
    }

    private void ReloadEnemyNumber()
    {
        enemyNumber = enemyManager.GenerateWave();
        uiManager.SetEnemyNumber(enemyNumber);
        uiManager.SetEnemyGroups();
    }
}
