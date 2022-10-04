using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private List<Color> coinsColors = new()
    {
        Color.red,
        Color.yellow,
        Color.green,
        Color.cyan,
        Color.blue,
        Color.magenta
    };

    public GameObject stageObject;
    public GameObject coinsObject;
    public GameObject startButtonObject;

    private SpriteRenderer stage;
    private SpriteRenderer coins;
    private GameManager gameManager;
    private Button startButton;

    // Start is called before the first frame update
    void Awake()
    {
        stage = stageObject.GetComponent<SpriteRenderer>();
        coins = coinsObject.GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        startButton = startButtonObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBattle()
    {
        startButton.interactable = false;
        stage.color = Color.red;
        gameManager.StartBattle();
    }

    public void SetEnemyNumber(int enemyNumber)
    {

    }

    public void EndBattle()
    {
        startButton.interactable = true;
        stage.color = Color.yellow;
    }

    public void SetCoinsNumber(int number)
    {
        coins.color = coinsColors[number];
    }
}
