using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    [HideInInspector]
    public Vector3 position;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    protected bool isSelected = false;

    protected void Initiate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        position = GetComponent<Transform>().position;
    }

    public abstract void Select();
    public abstract void CancelSelection();
}
