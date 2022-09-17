using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private GameObject createdInterface;
    private Cell selectedCell;
    private TowerManager towerManager;

    public GameObject towerPickingInterface;

    // Start is called before the first frame update
    void Start()
    {
        towerManager = GameObject.FindWithTag("TowerManager").GetComponent<TowerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select(Cell cell)
    {
        if (selectedCell != null)
        {
            Destroy(createdInterface);
            selectedCell.isSelected = false;
            selectedCell.spriteRenderer.color = Color.white;
        }

        selectedCell = cell;
        selectedCell.isSelected = true;
        selectedCell.spriteRenderer.color = Color.blue;
        Vector3 position = new(selectedCell.position.x, selectedCell.position.y, -1);
        createdInterface = Instantiate(towerPickingInterface, position, towerPickingInterface.transform.rotation);
    }

    public void CancelSelection()
    {
        if (selectedCell != null)
        {
            Destroy(createdInterface);
            selectedCell.isSelected = false;
            selectedCell.spriteRenderer.color = Color.white;
            selectedCell = null;
        }
    }

    public void PlaceTower(Tower.Type type)
    {
        towerManager.PlaceTower(type, selectedCell.position);
        CancelSelection();
    }
}
