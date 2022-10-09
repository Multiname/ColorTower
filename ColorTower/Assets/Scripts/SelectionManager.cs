using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private GameObject createdInterface;
    private Selectable selected;
    private TowerManager towerManager;
    private UIManager uiManager;

    public GameObject towerPickingInterface;
    public GameObject rangeVisualisation;

    // Start is called before the first frame update
    void Start()
    {
        towerManager = GameObject.FindWithTag("TowerManager").GetComponent<TowerManager>();
        uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Select(Selectable selectable)
    {
        if (selected != null)
        {
            Destroy(createdInterface);
            selected.CancelSelection();
        }

        selected = selectable;
        selected.Select();
    }

    public void SelectCell(Cell cell)
    {
        Select(cell);
        Vector3 position = new(selected.position.x, selected.position.y, -4);
        createdInterface = Instantiate(towerPickingInterface, position, towerPickingInterface.transform.rotation);
    }

    public void SelectTower(Tower tower)
    {
        Select(tower);
        Vector3 position = new(selected.position.x, selected.position.y, -1);
        createdInterface = Instantiate(rangeVisualisation, position, rangeVisualisation.transform.rotation);
        uiManager.SelectTower(tower);
    }

    public void CancelSelection()
    {
        if (selected != null)
        {
            Destroy(createdInterface);
            selected.CancelSelection();
            selected = null;
            uiManager.CancelSelection();
        }
    }

    public void PlaceTower(TypeManager.Type type)
    {
        towerManager.PlaceTower(type, selected.position);
        CancelSelection();
    }
}
