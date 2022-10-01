using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    public GameObject stageObject = null;

    private SpriteRenderer stage;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        stage = stageObject.GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBattle()
    {
        stage.color = Color.red;
        gameManager.StartBattle();
    }

    public void SetEnemyNumber(int enemyNumber)
    {

    }

    public void EndBattle()
    {
        stage.color = Color.yellow;
    }
}
