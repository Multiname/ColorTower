using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    public GameObject enemyPrefab;

    private TypeManager typeManager;
    private UIManager uiManager;
    private GameManager gameManager;

    public readonly Func<Transform, float, bool>[] moveInDirection = {
        (Transform transform, float speed) =>
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
            return transform.position.y <= -2;
        },
        (Transform transform, float speed) =>
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            return transform.position.x >= -3;
        },
        (Transform transform, float speed) =>
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
            return transform.position.y >= 2;
        },
        (Transform transform, float speed) =>
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            return transform.position.x >= 0;
        },
        (Transform transform, float speed) =>
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            return transform.position.x >= 4;
        }
    };
    public readonly int[][] movesets =
    {
        new[] { 0, 1, 2, 4, 0 },
        new[] { 0, 1, 2, 3, 0, 4 }
    };

    public float spawnInterval = 1;

    [HideInInspector]
    public TypeManager.Type[] currentEnemyType;
    [HideInInspector]
    public int[] enemyNumber;

    private int currentEnemyNumber;
    private int enemyHealthPoints = 5;
    private int rewardCoins = 1;
    private List<Enemy> enemies = new();

    private void Awake()
    {
        typeManager = GameObject.FindWithTag("TypeManager").GetComponent<TypeManager>();
        uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        currentEnemyType = new TypeManager.Type[3];
        enemyNumber = new int[3];
        for (int i = 0; i < 3; ++i)
            enemyNumber[i] = 0;

        ReloadEnemyNumber();
    }

    public int GenerateWave()
    {
        int enemyNumberSum = 0;
        for (int i = 0; i < 3; ++i)
        {
            currentEnemyType[i] = (TypeManager.Type)UnityEngine.Random.Range(0, 10);
            enemyNumberSum += enemyNumber[i];
        }

        if (enemyNumberSum >= 30)
        {
            enemyHealthPoints += 5;
            rewardCoins += 1;

            for (int i = 0; i < 3; ++i)
                enemyNumber[i] = 0;
            enemyNumberSum = 0;
        }

        enemyNumber[enemyNumberSum / 10] += 5;

        return enemyNumberSum + 5;
    }

    public IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < 3; ++i)
            for (int j = 0; j < enemyNumber[i]; ++j)
            {
                SpawnEnemy(i);
                yield return new WaitForSeconds(spawnInterval);
            }
    }

    private void SpawnEnemy(int enemyGroup)
    {
        Enemy enemy = Instantiate(enemyPrefab).GetComponent<Enemy>();
        enemy.MaxHealthPoints = enemyHealthPoints;
        enemy.movesetNumber = UnityEngine.Random.Range(0, 2);
        typeManager.SetType(currentEnemyType[enemyGroup], enemy);
        enemy.coins = rewardCoins;

        enemies.Add(enemy);
    }

    public void StopEnemies()
    {
        StopCoroutine(SpawnEnemies());
        foreach (Enemy enemy in enemies)
            Destroy(enemy);
    }

    public void DecrementEnemyNumber()
    {
        --currentEnemyNumber;
        if (currentEnemyNumber <= 0)
        {
            gameManager.EndBattle();
            ReloadEnemyNumber();
            return;
        }
        uiManager.SetEnemyNumber(currentEnemyNumber);
    }

    public void ReloadEnemyNumber()
    {
        enemies.Clear();
        currentEnemyNumber = GenerateWave();
        uiManager.SetEnemyNumber(currentEnemyNumber);
        uiManager.SetEnemyGroups();
    }
}
