using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public Animator animator;

    private EnemyManager enemyManager;
    private CoinManager coinManager;

    private Transform healthBarLength;
    private SpriteRenderer healthBarSprite;

    public float speed;

    [HideInInspector]
    public int movesetNumber;
    public int MaxHealthPoints {
        set
        {
            healthPoints = value;
            scaleChange = new(1.0f / value, 0, 0);
        }
    }
    [HideInInspector]
    public int coins;
    [HideInInspector]
    public TypeManager.Type type;

    private int movementStep = 0;
    private int healthPoints;
    private Vector3 scaleChange;
    private bool isAlive = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();
        coinManager = GameObject.FindWithTag("CoinManager").GetComponent<CoinManager>();

        Transform child = transform.GetChild(0);
        healthBarLength = child.transform;
        healthBarSprite = child.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (enemyManager.moveInDirection[enemyManager.movesets[movesetNumber][movementStep]](transform, speed))
            ++movementStep;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Core"))
        {
            collision.gameObject.GetComponent<Core>().TakeDamage();
            enemyManager.DecrementEnemyNumber();
            Destroy(gameObject);
            Destroy(this);
        }
    }

    public void TakeDamage(int projectileDamage, TypeManager.Type projectileType)
    {
        int damage = CalculateDamage(projectileDamage, projectileType);
        healthPoints -= damage;
        if (healthPoints <= 0)
        {
            if (!isAlive)
                return;
            isAlive = false;

            coinManager.ObtainCoins(coins);
            enemyManager.DecrementEnemyNumber();
            Destroy(gameObject);
        }
        else
        {
            healthBarSprite.enabled = true;
            healthBarLength.localScale -= scaleChange * damage;
        }
    }

    private int CalculateDamage(int damage, TypeManager.Type projectileType)
    {
        int projectileTypeNumber = (int)projectileType;
        int enemyTypeNumber = (int)type;
        if (Mathf.Abs(projectileTypeNumber / 4 - enemyTypeNumber / 4) != 0)
            return 0;
        if (enemyTypeNumber + projectileTypeNumber == 17)
            return (int)(damage * 1.5f);

        float gain = Mathf.Sin(enemyTypeNumber * Mathf.PI / 2 - projectileTypeNumber * Mathf.PI / 2) * damage * 0.5f;
        return damage + (int)gain;
    }
}
