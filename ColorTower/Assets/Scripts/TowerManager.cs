using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private List<Tower> createdTowers = new();
    private TypeManager typeManager;
    private CoinManager coinManager;

    public GameObject towerPrefab;
    public int towerCost = 1;

    // Start is called before the first frame update
    void Start()
    {
        typeManager = GameObject.FindWithTag("TypeManager").GetComponent<TypeManager>();
        coinManager = GameObject.FindWithTag("CoinManager").GetComponent<CoinManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
