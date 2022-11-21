using UnityEngine;
using UnityEngine.UI;

public class Core : MonoBehaviour
{
    [SerializeField]
    private Text healthPointsText;

    private GameManager gameManager;

    public int maxHealthPoints;

    [HideInInspector]
    public int healthPoints;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        healthPoints = maxHealthPoints;
        healthPointsText.text = healthPoints.ToString();
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
