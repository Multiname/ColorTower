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

    private Vector2 baseSpriteSize;

    private void Awake()
    {
        Initiate();
        weapon = transform.Find("Weapon").GetComponent<Weapon>();
        baseSpriteSize = spriteRenderer.size;
    }

    public override void CancelSelection()
    {
        isSelected = false;
        spriteRenderer.size = baseSpriteSize;
        transform.position = position;
    }

    public override void Select()
    {
        isSelected = true;
        spriteRenderer.size = baseSpriteSize * 1.2f;
        Vector3 newPosition = position;
        newPosition.z = -2;
        transform.position = newPosition;
    }

    private void OnMouseEnter()
    {
        if (!isSelected)
        {
            spriteRenderer.size = baseSpriteSize * 1.1f;
            Vector3 newPosition = position;
            newPosition.z = -3;
            transform.position = newPosition;
        }
    }

    private void OnMouseExit()
    {
        if (!isSelected)
        {
            spriteRenderer.size = baseSpriteSize;
            transform.position = position;
        }
    }
}
