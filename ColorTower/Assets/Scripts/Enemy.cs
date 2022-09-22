using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyManager enemyManager;
    public int movesetNumber;
    private int movementStep = 0;
    public float speed = 5.0f;

    public TypeManager.Type type;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyManager.moveInDirection[enemyManager.movesets[movesetNumber][movementStep]](transform, speed))
            ++movementStep;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Core"))
            Destroy(gameObject);
    }
}
