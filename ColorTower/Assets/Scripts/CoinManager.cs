using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coins = 3;

    private UIManager uiManager;
    private SelectionManager selectionManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        selectionManager = GameObject.FindWithTag("SelectionManager").GetComponent<SelectionManager>();

        uiManager.SetCoinsNumber(coins);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pay(int cost)
    {
        coins -= cost;
        uiManager.SetCoinsNumber(coins);
    }

    public void ObtainCoins(int cost)
    {
        coins += cost;
        uiManager.SetCoinsNumber(coins);
    }

    public int CalculateTowerDamageUpgradeCost(int currentDamage)
    {
        return currentDamage + 1;
    }

    public int CalculateTowerRangeUpgradeCost(int currentRange)
    {
        return currentRange * 10;
    }

    public void UpgradeTowerDamage(Weapon weapon)
    {
        Pay(CalculateTowerDamageUpgradeCost(weapon.damage));
        ++weapon.damage;
    }

    public void UpgradeTowerRange(Tower tower)
    {
        Pay(CalculateTowerRangeUpgradeCost(tower.range));
        tower.weapon.rangeCollider.radius = 2.5f + tower.range * 0.8f;
        ++tower.range;
        selectionManager.CancelSelection();
        selectionManager.SelectTower(tower);
    }
}
