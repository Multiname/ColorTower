using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    private List<Color> healthPointsColors = new()
    {
        Color.red,
        Color.yellow,
        Color.green,
        Color.white
    };

    public int maxHealthPoints = 4;

    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    public int healthPoints;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        healthPoints = maxHealthPoints;
        spriteRenderer.color = healthPointsColors[healthPoints - 1];

        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage()
    {
        --healthPoints;
        if (healthPoints <= 0)
        {
            gameManager.EndGame();
            Destroy(gameObject);
        }
        else
            spriteRenderer.color = healthPointsColors[healthPoints - 1];
    }
}
