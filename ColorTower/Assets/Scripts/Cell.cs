using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : Selectable
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Initiate();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    public override void CancelSelection()
    {
        isSelected = false;
        spriteRenderer.color = Color.white;
    }

    public override void Select()
    {
        isSelected = true;
        spriteRenderer.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if (!isSelected && gameManager.gameState == GameManager.GameState.Preparation)
            if (coinManager.Coins <= 0)
                spriteRenderer.color = Color.gray;
            else
                spriteRenderer.color = Color.green;
    }

    private void OnMouseExit()
    {
        if (!isSelected && gameManager.gameState == GameManager.GameState.Preparation)
            spriteRenderer.color = Color.white;
    }

    private void OnMouseDown()
    {
        if (!isSelected && gameManager.gameState == GameManager.GameState.Preparation && coinManager.Coins > 0)
            selectionManager.SelectCell(this);
    }
}
