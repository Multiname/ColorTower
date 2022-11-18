using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Core : MonoBehaviour
{
    public int maxHealthPoints;
    public Text healthPointsText;

    private GameManager gameManager;
    public int healthPoints;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        healthPoints = maxHealthPoints;
        healthPointsText.text = healthPoints.ToString();
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
            Destroy(healthPointsText.gameObject);
            Destroy(gameObject);
        }
        else
            healthPointsText.text = healthPoints.ToString();
    }
}
