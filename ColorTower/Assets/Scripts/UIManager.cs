using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text coinsNumber;
    [SerializeField]
    private Text selectedTowerDamage;
    [SerializeField]
    private Text selectedTowerDamageIncrease;
    [SerializeField]
    private Text selectedTowerRange;
    [SerializeField]
    private Button towerDamageUpgradeButton;
    [SerializeField]
    private Text towerDamageUpgradeText;
    [SerializeField]
    private Button towerRangeUpgradeButton;
    [SerializeField]
    private Text towerRangeUpgradeText;
    [SerializeField]
    private SpriteRenderer stage;
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private SpriteRenderer selectedTowerSprite;
    [SerializeField]
    private Text enemyNumberText;
    [SerializeField]
    private Text waveNumber;
    [SerializeField]
    private GameObject gameOverWindowPrefab;
    [SerializeField]
    private SpriteRenderer[] enemyGroups = new SpriteRenderer[3];
    [SerializeField]
    private Text[] enemyGroupTexts = new Text[3];
    [SerializeField]
    private Sprite preparationStageSprite;
    [SerializeField]
    private Sprite battleStageSprite;

    private Sprite[] groupSprites = new Sprite[11];

    private TypeManager typeManager;
    private GameManager gameManager;
    private CoinManager coinManager;
    private EnemyManager enemyManager;

    private readonly Color[] groupTextColors = {
        new Color(0.082f, 0.161f, 0.094f),
        new Color(0.631f, 0.471f, 0.031f),
        new Color(0.486f, 0.125f, 0.027f),
        new Color(0.016f, 0.204f, 0.263f),
        new Color(0.157f, 0.396f, 0.063f),
        new Color(0.173f, 0.122f, 0.055f),
        new Color(0.259f, 0.090f, 0.255f),
        new Color(0.031f, 0.518f, 0.518f),
        new Color(0.078f, 0.090f, 0.098f),
        new Color(0.000f, 0.922f, 0.659f)
    };

    private Tower selectedTower = null;

    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        typeManager = GameObject.FindWithTag("TypeManager").GetComponent<TypeManager>();
        coinManager = GameObject.FindWithTag("CoinManager").GetComponent<CoinManager>();
        enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();

        groupSprites = Resources.LoadAll<Sprite>("Sprites/Map/sprite_group");
    }

    public void StartBattle()
    {
        startButton.interactable = false;
        stage.sprite = battleStageSprite;
        gameManager.StartBattle();
        if (selectedTower != null)
        {
            UpdateDamageUpgradeButton();
            UpdateRangeUpgradeButton();
        }
    }

    public void SetEnemyNumber(int enemyNumber)
    {
        enemyNumberText.text = enemyNumber.ToString();
    }

    public void SetEnemyGroups()
    {
        for (int i = 0; i < 3; ++i)
            if (enemyManager.enemyNumber[i] > 0)
            {
                int typeNumber = (int)enemyManager.currentEnemyType[i];
                enemyGroups[i].sprite = groupSprites[typeNumber];
                enemyGroupTexts[i].text = enemyManager.enemyNumber[i].ToString();
                enemyGroupTexts[i].color = groupTextColors[typeNumber];
            }
            else
            {
                enemyGroups[i].sprite = groupSprites[10];
                enemyGroupTexts[i].text = "";
            }
    }

    public void EndBattle()
    {
        startButton.interactable = true;
        stage.sprite = preparationStageSprite;
        if (selectedTower != null)
        {
            UpdateDamageUpgradeButton();
            UpdateRangeUpgradeButton();
        }
    }

    public void SetCoinsNumber(int number)
    {
        coinsNumber.text = number.ToString();
    }

    private void UpdateDamageUpgradeButton()
    {
        selectedTowerDamage.text = selectedTower.weapon.damage.ToString();
        int damageUpgradeCost = coinManager.CalculateTowerDamageUpgradeCost(selectedTower.weapon.damage);
        towerDamageUpgradeText.text = damageUpgradeCost.ToString();
        towerDamageUpgradeButton.interactable = (coinManager.coins >= damageUpgradeCost) && gameManager.gameState == GameManager.GameState.Preparation;
    }

    private void UpdateRangeUpgradeButton()
    {
        selectedTowerRange.text = selectedTower.range.ToString();
        if (selectedTower.range >= 3)
        {
            towerRangeUpgradeButton.interactable = false;
            return;
        }
        int rangeUpgrade = coinManager.CalculateTowerRangeUpgradeCost(selectedTower.range);
        towerRangeUpgradeText.text = rangeUpgrade.ToString();
        towerRangeUpgradeButton.interactable = (coinManager.coins >= rangeUpgrade) && gameManager.gameState == GameManager.GameState.Preparation;
    }

    public void SelectTower(Tower tower)
    {
        selectedTower = tower;

        selectedTowerSprite.sprite = typeManager.towerSprites[(int)selectedTower.weapon.currentType];
        selectedTowerDamageIncrease.text = "x2";

        UpdateDamageUpgradeButton();
        UpdateRangeUpgradeButton();
    }

    public void CancelTowerSelection()
    {
        if (selectedTower == null)
            return;

        selectedTowerSprite.sprite = typeManager.towerSprites[10];
        selectedTowerDamage.text = "";
        selectedTowerRange.text = "";
        selectedTowerDamageIncrease.text = "";
        towerDamageUpgradeText.text = "";
        towerRangeUpgradeText.text = "";
        towerDamageUpgradeButton.interactable = false;
        towerRangeUpgradeButton.interactable = false;

        selectedTower = null;
    }

    public void UpgradeDamage()
    {
        coinManager.UpgradeTowerDamage(selectedTower.weapon);
        UpdateDamageUpgradeButton();
    }

    public void UpgradeRange()
    {
        coinManager.UpgradeTowerRange(selectedTower);
        UpdateRangeUpgradeButton();
    }

    public void SetWaveNumber(int number)
    {
        waveNumber.text = number.ToString();
    }

    public void ShowGameOverWindow(int finalWaveNumber)
    {
        GameObject gameOverWindow = Instantiate(gameOverWindowPrefab);
        Transform canvas = gameOverWindow.transform.GetChild(0);
        canvas.GetComponent<Canvas>().worldCamera = Camera.main;
        canvas.GetChild(0).GetComponent<Text>().text = finalWaveNumber.ToString();
        canvas.GetChild(1).GetComponent<Button>().onClick.AddListener(ReturnToMenu);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
