using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private List<GameObject> createdTowers = new();

    public GameObject towerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceTower(TypeManager.Type type, Vector3 cellPosition)
    {
        Vector3 towerPosition = new(cellPosition.x, cellPosition.y, 0);
        GameObject createdTower = Instantiate(towerPrefab, towerPosition, towerPrefab.transform.rotation);
        createdTower.GetComponent<Tower>().SetType(type);
        createdTowers.Add(createdTower);
    }
}
