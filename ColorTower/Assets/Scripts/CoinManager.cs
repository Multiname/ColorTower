using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private UIManager uiManager;
    private SelectionManager selectionManager;

    public int coins;

    private void Start()
    {
        uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        selectionManager = GameObject.FindWithTag("SelectionManager").GetComponent<SelectionManager>();

        uiManager.SetCoinsNumber(coins);
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
