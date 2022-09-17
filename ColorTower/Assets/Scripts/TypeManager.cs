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

    private List<Color> typeColors = new() {
        Color.green, Color.yellow, Color.red, Color.blue,
        new Color(199, 234, 70), new Color(253, 106, 2), new Color(75, 0, 130), Color.cyan,
        Color.black, new Color(219, 241, 239)};

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetType(Type type, SpriteRenderer spriteRenderer)
    {
        spriteRenderer.color = typeColors[((int)type)];
    }
}
