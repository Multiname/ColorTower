using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Vector3 position;
    public bool isSelected = false;

    private SelectionManager selectionManager;
    private GameManager gameManager;
    private CoinManager coinManager;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        position = GetComponent<Transform>().position;
        selectionManager = GameObject.FindWithTag("SelectionManager").GetComponent<SelectionManager>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        coinManager = GameObject.FindWithTag("CoinManager").GetComponent<CoinManager>();
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
            selectionManager.Select(this);
    }
}
