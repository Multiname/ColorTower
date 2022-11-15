using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : Selectable
{
    private GameManager gameManager;
    private TypeManager typeManager;

    // Start is called before the first frame update
    void Start()
    {
        Initiate();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        typeManager = GameObject.FindWithTag("TypeManager").GetComponent<TypeManager>();
    }

    public override void CancelSelection()
    {
        isSelected = false;
        spriteRenderer.sprite = typeManager.unselectedCellSprite;
    }

    public override void Select()
    {
        isSelected = true;
        spriteRenderer.sprite = typeManager.selectedCellSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if (!isSelected && gameManager.gameState == GameManager.GameState.Preparation)
            if (coinManager.coins <= 0)
                spriteRenderer.sprite = typeManager.blockedCellSprite;
            else
                spriteRenderer.sprite = typeManager.hoveredCellSprite;
    }

    private void OnMouseExit()
    {
        if (!isSelected && gameManager.gameState == GameManager.GameState.Preparation)
            spriteRenderer.sprite = typeManager.unselectedCellSprite;
    }
}
