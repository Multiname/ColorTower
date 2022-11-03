using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeManager : MonoBehaviour
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

    public readonly List<Color> typeColors = new() {
        Color.green, Color.yellow, Color.red, Color.blue,
        new Color(0.68f, 0.84f, 0.03f), new Color(0.93f, 0.54f, 0.07f), new Color(0.8f, 0.12f, 0.69f), Color.cyan,
        new Color(0.2f, 0.2f, 0.2f), new Color(0.8f, 0.8f, 0.8f)};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetType(Type type, Tower tower, bool isOriginal)
    {
        if (isOriginal)
            tower.weapon.originalType = type;
        tower.weapon.currentType = type;
        tower.spriteRenderer.color = typeColors[(int)type];
    }

    public void SetType(Type type, Enemy enemy)
    {
        enemy.type = type;
        enemy.spriteRenderer.color = typeColors[(int)type];
    }

    public void SetType(Type type, Projectile projectile)
    {
        projectile.type = type;
        projectile.SpriteRenderer.color = typeColors[(int)type];
    }

    public void ColorConnection(SpriteRenderer connection, Type type)
    {
        connection.color = typeColors[(int)type] + new Color(0.1f, 0.1f, 0.1f);
    }
}
