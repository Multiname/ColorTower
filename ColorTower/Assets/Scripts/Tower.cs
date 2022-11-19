using UnityEngine;

public class Tower : Selectable
{
    [HideInInspector]
    public Weapon weapon;
    [HideInInspector]
    public Tower connectedWith = null;
    [HideInInspector]
    public GameObject connection = null;

    [HideInInspector]
    public int range = 1;

    private void Awake()
    {
        Initiate();
        weapon = transform.Find("Weapon").GetComponent<Weapon>();
    }

    public override void CancelSelection()
    {
        isSelected = false;
        transform.localScale = new(1, 1, 1);
        transform.position = position;
    }

    public override void Select()
    {
        isSelected = true;
        transform.localScale = new(1.2f, 1.2f, 1);
        Vector3 newPosition = position;
        newPosition.z = -2;
        transform.position = newPosition;
    }

    private void OnMouseEnter()
    {
        if (!isSelected)
        {
            transform.localScale = new(1.1f, 1.1f, 1);
            Vector3 newPosition = position;
            newPosition.z = -3;
            transform.position = newPosition;
        }
    }

    private void OnMouseExit()
    {
        if (!isSelected)
        {
            transform.localScale = new(1, 1, 1);
            transform.position = position;
        }
    }
}
