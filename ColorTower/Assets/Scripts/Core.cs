using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    private List<Color> healthPointsColors = new()
    {
        Color.white,
        Color.green,
        Color.yellow,
        Color.red
    };

    public int maxHealthPoints = 4;

    private SpriteRenderer spriteRenderer;
    private int healthPoints;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = healthPointsColors[maxHealthPoints - healthPoints];

        healthPoints = maxHealthPoints;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage()
    {
        --healthPoints;
        if (healthPoints <= 0)
            Destroy(gameObject);
        else
            spriteRenderer.color = healthPointsColors[maxHealthPoints - healthPoints];
    }
}
