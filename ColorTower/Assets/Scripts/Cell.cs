using UnityEngine;

public class Cell : Selectable
{
    private GameManager gameManager;
    private TypeManager typeManager;
    private CoinManager coinManager;

    private void Start()
    {
        Initiate();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        typeManager = GameObject.FindWithTag("TypeManager").GetComponent<TypeManager>();
        coinManager = GameObject.FindWithTag("CoinManager").GetComponent<CoinManager>();
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

    private void OnMouseEnter()
    {
        if (!isSelected && gameManager.gameState == GameManager.GameState.Preparation)
            if (coinManager.coins < 10)
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
