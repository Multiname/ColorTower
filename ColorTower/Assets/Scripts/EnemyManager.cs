using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public readonly List<Func<Transform, float, bool>> moveInDirection = new() {
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
    public readonly List<List<int>> movesets = new()
    {
        new() { 0, 1, 2, 4, 0 },
        new() { 0, 1, 2, 3, 0, 4 }
    };

    public GameObject enemyPrefab;
    public TypeManager.Type currentEnemyType;
    public int enemyNumber = 5;
    public float spawnInterval = 1;

    private TypeManager typeManager;

    // Start is called before the first frame update
    void Start()
    {
        typeManager = GameObject.FindWithTag("TypeManager").GetComponent<TypeManager>();

        GenerateWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateWave()
    {
        currentEnemyType = (TypeManager.Type)UnityEngine.Random.Range(0, 4);
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
        enemy.movesetNumber = UnityEngine.Random.Range(0, 2);
    }
}
