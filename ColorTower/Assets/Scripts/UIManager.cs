using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text coinsNumber;
    public Text selectedTowerDamage;
    public Text selectedTowerDamageIncrease;
    public Text selectedTowerRange;
    public Button towerDamageUpgradeButton;
    public Text towerDamageUpgradeText;
    public Button towerRangeUpgradeButton;
    public Text towerRangeUpgradeText;
    public SpriteRenderer stage;
    public Button startButton;
    public SpriteRenderer selectedTowerSprite;
    public Text enemyNumberText;

    public SpriteRenderer[] enemyGroups = new SpriteRenderer[3];
    public Text[] enemyGroupTexts = new Text[3];

    private TypeManager typeManager;
    private GameManager gameManager;
    private CoinManager coinManager;
    private EnemyManager enemyManager;

    private Tower selectedTower = null;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        typeManager = GameObject.FindWithTag("TypeManager").GetComponent<TypeManager>();
        coinManager = GameObject.FindWithTag("CoinManager").GetComponent<CoinManager>();
        enemyManager = GameObject.FindWithTag("EnemyManager").GetComponent<EnemyManager>();

        selectedTowerSprite.color = Color.gray;
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
                enemyGroups[i].enabled = true;
                enemyGroups[i].color = typeManager.typeColors[(int)enemyManager.currentEnemyType[i]];
                enemyGroupTexts[i].text = enemyManager.enemyNumber[i].ToString();
            }
            else
            {
                enemyGroups[i].enabled = false;
                enemyGroupTexts[i].text = "";
            }
    }

    public void EndBattle()
    {
        startButton.interactable = true;
        stage.color = Color.yellow;
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

        selectedTowerSprite.color = typeManager.typeColors[(int)selectedTower.weapon.type];
        selectedTowerDamageIncrease.text = "+1";

        UpdateDamageUpgradeButton();
        UpdateRangeUpgradeButton();
    }

    public void CancelTowerSelection()
    {
        if (selectedTower == null)
            return;

        selectedTowerSprite.color = Color.gray;
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
}
