using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    public Vector3 position;
    public SpriteRenderer spriteRenderer;
    public bool isSelected = false;

    protected SelectionManager selectionManager;
    protected CoinManager coinManager;

    protected void Initiate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        position = GetComponent<Transform>().position;

        selectionManager = GameObject.FindWithTag("SelectionManager").GetComponent<SelectionManager>();
        coinManager = GameObject.FindWithTag("CoinManager").GetComponent<CoinManager>();
    }

    public abstract void Select();
    public abstract void CancelSelection();

    // Update is called once per frame
    void Update()
    {
        
    }
}
