using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Vector3 position;
    public bool isSelected = false;

    private SelectionManager selectionManager;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        position = GetComponent<Transform>().position;
        selectionManager = GameObject.FindWithTag("SelectionManager").GetComponent<SelectionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if (!isSelected)
            spriteRenderer.color = Color.green;
    }

    private void OnMouseExit()
    {
        if (!isSelected)
            spriteRenderer.color = Color.white;
    }

    private void OnMouseDown()
    {
        if (!isSelected)
            selectionManager.Select(this);
    }
}
