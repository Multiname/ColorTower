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
    private TypeManager typeManager;

    public GameObject towerPickingInterface;
    public GameObject rangeVisualisation;
    public GameObject connectionVisualisation;
    public CoinManager coinManager;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        towerManager = GameObject.FindWithTag("TowerManager").GetComponent<TowerManager>();
        uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        coinManager = GameObject.FindWithTag("CoinManager").GetComponent<CoinManager>();
        typeManager = GameObject.FindWithTag("TypeManager").GetComponent<TypeManager>();
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
                    SelectOrChangeConnectionTower(rayHit.transform.GetComponent<Tower>());
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

    private bool ConnectTowers(Tower secondTower)
    {
        Tower selectedTower = selected.GetComponent<Tower>();
        int firstTypeNumber = (int)selectedTower.weapon.currentType;
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

        typeManager.SetType(resultType, selectedTower, false);
        typeManager.SetType(resultType, secondTower, false);
        selectedTower.connectedWith = secondTower;
        secondTower.connectedWith = selectedTower;
        selectedTower.connection = Instantiate(connectionVisualisation,
            (selectedTower.position + secondTower.position) / 2,
            Quaternion.LookRotation(Vector3.forward, selectedTower.position - secondTower.position));
        secondTower.connection = selectedTower.connection;
        typeManager.ColorConnection(selectedTower.connection.GetComponent<SpriteRenderer>(), resultType);

        return true;
    }

    private void SelectOrChangeConnectionTower(Tower tower)
    {
        if (selected != null)
        {
            if (selected.GetComponent<Tower>().connectedWith == tower)
            {
                UnconnectTower();
                CancelSelection();
                return;
            }
            if (Vector2.Distance(selected.position, tower.position) <= 1)
                if (ConnectTowers(tower))
                {
                    CancelSelection();
                    return;
                }
        }

        SelectTower(tower);
    }

    private void UnconnectTower()
    {
        Tower tower = selected.GetComponent<Tower>();
        typeManager.SetType(tower.weapon.originalType, tower, false);
        typeManager.SetType(tower.connectedWith.weapon.originalType, tower.connectedWith, false);
        Destroy(tower.connection);
        tower.connection = null;
        tower.connectedWith.connection = null;
        tower.connectedWith.connectedWith = null;
        tower.connectedWith = null;
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
