using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPicker : MonoBehaviour
{
    private SelectionManager selectionManager;

    public TypeManager.Type type;

    // Start is called before the first frame update
    void Start()
    {
        selectionManager = GameObject.FindWithTag("SelectionManager").GetComponent<SelectionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
