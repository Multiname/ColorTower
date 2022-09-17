using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private List<GameObject> createdTowers = new();

    public readonly List<Color> towerColors = new() {
        Color.green, Color.yellow, Color.red, Color.blue,
        new Color(199, 234, 70), new Color(253, 106, 2), new Color(75, 0, 130), Color.cyan,
        Color.black, new Color(219, 241, 239)};
    public GameObject towerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceTower(Tower.Type type, Vector3 cellPosition)
    {
        Vector3 towerPosition = new(cellPosition.x, cellPosition.y, 0);
        GameObject createdTower = Instantiate(towerPrefab, towerPosition, towerPrefab.transform.rotation);
        createdTower.GetComponent<Tower>().SetType(type);
        createdTowers.Add(createdTower);
    }
}
