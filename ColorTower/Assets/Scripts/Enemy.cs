using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private TypeManager.Type type;
    private SpriteRenderer spriteRenderer;
    private Transform transform;
    private TypeManager typeManager;
    private EnemyManager enemyManager;
    public int movesetNumber;
    private int movementStep = 0;
    public float speed = 5.0f;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
        typeManager = GameObject.FindWithTag("TypeManager").GetComponent<TypeManager>();
        enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyManager.moveInDirection[enemyManager.movesets[movesetNumber][movementStep]](transform, speed))
            ++movementStep;
    }

    public void SetType(TypeManager.Type type)
    {
        this.type = type;
        typeManager.SetType(this.type, spriteRenderer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
