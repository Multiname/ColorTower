using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public TypeManager.Type currentEnemyType;
    public int enemyNumber = 5;
    public float spawnInterval = 1;

    private TypeManager typeManager;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        typeManager = GameObject.FindWithTag("TypeManager").GetComponent<TypeManager>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        GenerateWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateWave()
    {
        currentEnemyType = (TypeManager.Type)Random.Range(0, 4);
    }

    public IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemyNumber - 1; ++i)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Enemy enemy = Instantiate(enemyPrefab).GetComponent<Enemy>();
        typeManager.SetType(currentEnemyType, enemy);
        enemy.movesetNumber = Random.Range(0, 2);
    }
}
