using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int Coins { get; private set; } = 3;

    private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();

        uiManager.SetCoinsNumber(Coins);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pay(int cost)
    {
        Coins -= cost;
        uiManager.SetCoinsNumber(Coins);
    }

    public void ObtainCoins(int cost)
    {
        Coins += cost;
        uiManager.SetCoinsNumber(Coins);
    }
}
