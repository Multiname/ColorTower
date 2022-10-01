using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum GameState
    {
        Preparation,
        Battle
    }

    private GameState gameState = GameState.Preparation;
    private SpawnManager spawnManager;
    private UIManager uiManager;
    private int enemyNumber;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManager>();
        uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();

        enemyNumber = spawnManager.enemyNumber;
        uiManager.SetEnemyNumber(enemyNumber);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBattle()
    {
        gameState = GameState.Battle;
        StartCoroutine(spawnManager.SpawnEnemies());
    }

    public void DecrementEnemyNumber()
    {
        --enemyNumber;
        uiManager.SetEnemyNumber(enemyNumber);
        if (enemyNumber <= 0)
        {
            gameState = GameState.Preparation;
            uiManager.EndBattle();
            spawnManager.GenerateWave();
            enemyNumber = spawnManager.enemyNumber;
        }
    }
}
