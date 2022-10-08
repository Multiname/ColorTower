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
    public GameObject selectedTowerObject;

    private SpriteRenderer stage;
    private SpriteRenderer coins;
    private GameManager gameManager;
    private Button startButton;
    private SpriteRenderer selectedTower;
    private TypeManager typeManager;

    // Start is called before the first frame update
    void Awake()
    {
        stage = stageObject.GetComponent<SpriteRenderer>();
        coins = coinsObject.GetComponent<SpriteRenderer>();
        selectedTower = selectedTowerObject.GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        typeManager = GameObject.FindWithTag("TypeManager").GetComponent<TypeManager>();
        startButton = startButtonObject.GetComponent<Button>();

        selectedTower.color = Color.gray;
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

    public void SelectTower(Tower tower)
    {
        selectedTower.color = typeManager.typeColors[(int)tower.weapon.type];
    }

    public void CancelSelection()
    {
        selectedTower.color = Color.gray;
    }
}
