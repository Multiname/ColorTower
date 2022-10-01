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
    public int healthPoints;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        healthPoints = maxHealthPoints;
        spriteRenderer.color = healthPointsColors[healthPoints - 1];
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
            spriteRenderer.color = healthPointsColors[healthPoints - 1];
    }
}
