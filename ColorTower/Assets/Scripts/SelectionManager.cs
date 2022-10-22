using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{
    private GameObject createdInterface;
    private Selectable selected;
    private TowerManager towerManager;
    private UIManager uiManager;

    public GameObject towerPickingInterface;
    public GameObject rangeVisualisation;
    public CoinManager coinManager;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        towerManager = GameObject.FindWithTag("TowerManager").GetComponent<TowerManager>();
        uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        coinManager = GameObject.FindWithTag("CoinManager").GetComponent<CoinManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
            switch (rayHit.transform.tag)
            {
                case "Cell":
                    if (gameManager.gameState == GameManager.GameState.Preparation && coinManager.coins > 0)
                        SelectCell(rayHit.transform.GetComponent<Cell>());
                    break;
                case "Tower":
                    SelectTower(rayHit.transform.GetComponent<Tower>());
                    break;
                case "TowerPicker":
                    PlaceTower(rayHit.transform.GetComponent<TowerPicker>().type);
                    break;
                default:
                    CancelSelection();
                    break;
            }
        }
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
        int radius = tower.range * 2 + 4;
        createdInterface.transform.localScale = new(radius, radius, 1);
        uiManager.SelectTower(tower);
    }

    public void CancelSelection()
    {
        if (selected != null)
        {
            Destroy(createdInterface);
            selected.CancelSelection();
            selected = null;
            uiManager.CancelTowerSelection();
        }
    }

    public void PlaceTower(TypeManager.Type type)
    {
        towerManager.PlaceTower(type, selected.position);
        CancelSelection();
    }
}
