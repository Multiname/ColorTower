using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyManager enemyManager;
    private GameManager gameManager;
    private CoinManager coinManager;
    public int movesetNumber;
    private int movementStep = 0;
    public float speed = 5.0f;
    public int maxHealthPoints;
    public int coins = 1;

    public int healthPoints;
    public TypeManager.Type type;
    public SpriteRenderer spriteRenderer;
    private Transform healthBarLength;
    private SpriteRenderer healthBarSprite;
    private Vector3 scaleChange;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        coinManager = GameObject.FindWithTag("CoinManager").GetComponent<CoinManager>();

        Transform child = transform.GetChild(0);
        healthBarLength = child.transform;
        healthBarSprite = child.GetComponent<SpriteRenderer>();
        
        healthPoints = maxHealthPoints;
        scaleChange = new(1.0f / maxHealthPoints, 0, 0);
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
        {
            collision.gameObject.GetComponent<Core>().TakeDamage();
            gameManager.DecrementEnemyNumber();
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            gameManager.DecrementEnemyNumber();
            coinManager.ObtainCoins(coins);
            Destroy(gameObject);
        }
        else
        {
            healthBarSprite.enabled = true;
            healthBarLength.localScale -= scaleChange * damage;
        }
    }
}
