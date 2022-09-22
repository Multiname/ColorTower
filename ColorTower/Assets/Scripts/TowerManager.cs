using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private List<GameObject> createdTowers = new();
    private TypeManager typeManager;

    public GameObject towerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        typeManager = GameObject.FindWithTag("TypeManager").GetComponent<TypeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceTower(TypeManager.Type type, Vector3 cellPosition)
    {
        Vector3 towerPosition = new(cellPosition.x, cellPosition.y, 0);
        GameObject createdTower = Instantiate(towerPrefab, towerPosition, towerPrefab.transform.rotation);
        typeManager.SetType(type, createdTower.GetComponent<Tower>());
        createdTowers.Add(createdTower);
    }
}
