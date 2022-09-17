using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public readonly List<Func<Transform, float, bool>> moveInDirection = new() {
        (Transform transform, float speed) =>
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
            return transform.position.y <= -2;
        },
        (Transform transform, float speed) =>
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            return transform.position.x >= -3;
        },
        (Transform transform, float speed) =>
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
            return transform.position.y >= 2;
        },
        (Transform transform, float speed) =>
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            return transform.position.x >= 0;
        },
        (Transform transform, float speed) =>
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            return transform.position.x >= 4;
        }
    };
    public readonly List<List<int>> movesets = new()
    {
        new() { 0, 1, 2, 4, 0 },
        new() { 0, 1, 2, 3, 0, 4 }
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
