using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum Type
    {
        Green,
        Yellow,
        Red,
        Blue,
        Lime,
        Orange,
        Purple,
        Cyan,
        Black,
        White
    }

    private Type type;
    private SpriteRenderer spriteRenderer;
    private TowerManager towerManager;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        towerManager = GameObject.FindWithTag("TowerManager").GetComponent<TowerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetType(Type type)
    {
        this.type = type;
        spriteRenderer.color = towerManager.towerColors[((int)this.type)];
    }
}
