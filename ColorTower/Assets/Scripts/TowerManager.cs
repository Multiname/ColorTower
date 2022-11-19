using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;
    [SerializeField]
    private GameObject connectionVisualisation;

    private TypeManager typeManager;
    private CoinManager coinManager;

    public int towerCost = 1;

    private List<Tower> createdTowers = new();

    private void Start()
    {
        typeManager = GameObject.FindWithTag("TypeManager").GetComponent<TypeManager>();
        coinManager = GameObject.FindWithTag("CoinManager").GetComponent<CoinManager>();
    }

    public void PlaceTower(TypeManager.Type type, Vector3 cellPosition)
    {
        Vector3 towerPosition = new(cellPosition.x, cellPosition.y, 0);
        Tower createdTower = Instantiate(towerPrefab, towerPosition, towerPrefab.transform.rotation).GetComponent<Tower>();
        coinManager.Pay(towerCost);
        typeManager.SetType(type, createdTower, true);
        createdTowers.Add(createdTower);
    }

    public void StopTowers()
    {
        foreach (Tower tower in createdTowers)
        {
            Destroy(tower.weapon);
            Destroy(tower);
        }
    }

    public bool ConnectTowers(Tower firstTower, Tower secondTower)
    {
        int firstTypeNumber = (int)firstTower.weapon.currentType;
        int secondTypeNumber = (int)secondTower.weapon.currentType;

        if (firstTypeNumber / 4 + secondTypeNumber / 4 != 0)
            return false;
        if (firstTypeNumber == secondTypeNumber)
            return false;

        TypeManager.Type resultType;
        if ((firstTypeNumber + secondTypeNumber) % 2 == 0)
            resultType = (TypeManager.Type)((firstTypeNumber + secondTypeNumber) / 2 + 7);
        else
        {
            float a = firstTypeNumber * Mathf.PI / 2;
            float b = secondTypeNumber * Mathf.PI / 2;

            int x = Mathf.RoundToInt(2 * (Mathf.Cos(a) + Mathf.Cos(b)) - (Mathf.Sin(a) + Mathf.Sin(b)));
            int sign = (int)((int)Mathf.Pow(2, x) * Mathf.Pow(2, -x));
            resultType = (TypeManager.Type)((int)(x * Mathf.Pow(-1, sign) + 4 * sign + 5) / 2 % 4 + 4);
        }

        typeManager.SetType(resultType, firstTower, false);
        typeManager.SetType(resultType, secondTower, false);
        firstTower.connectedWith = secondTower;
        secondTower.connectedWith = firstTower;
        firstTower.connection = Instantiate(connectionVisualisation,
            (firstTower.position + secondTower.position) / 2 + Vector3.forward,
            Quaternion.LookRotation(Vector3.forward, firstTower.position - secondTower.position));
        secondTower.connection = firstTower.connection;
        typeManager.ColorConnection(firstTower.connection.GetComponent<SpriteRenderer>(), resultType);

        return true;
    }

    public void UnconnectTower(Tower tower)
    {
        typeManager.SetType(tower.weapon.originalType, tower, false);
        typeManager.SetType(tower.connectedWith.weapon.originalType, tower.connectedWith, false);
        Destroy(tower.connection);
        tower.connection = null;
        tower.connectedWith.connection = null;
        tower.connectedWith.connectedWith = null;
        tower.connectedWith = null;
    }
}
