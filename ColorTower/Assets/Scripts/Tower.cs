using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private TypeManager.Type type;
    private SpriteRenderer spriteRenderer;
    private TypeManager typeManager;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        typeManager = GameObject.FindWithTag("TypeManager").GetComponent<TypeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetType(TypeManager.Type type)
    {
        this.type = type;
        typeManager.SetType(this.type, spriteRenderer);
    }
}
