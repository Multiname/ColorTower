using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectilePrefab;
    public TypeManager.Type type;
    public float cooldown = 1.0f;
    public int damage = 1;
    public CircleCollider2D collider;

    private Vector3 position;
    private TypeManager typeManager;
    private Queue<Transform> targets = new();

    // Start is called before the first frame update
    void Awake()
    {
        position = transform.parent.position;
        position.z = -1;
        typeManager = GameObject.FindWithTag("TypeManager").GetComponent<TypeManager>();
        collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            targets.Enqueue(collision.transform);
            if (targets.Count == 1)
                InvokeRepeating(nameof(Fire), 0, cooldown);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            targets.Dequeue();
            if (targets.Count == 0)
                CancelInvoke(nameof(Fire));
        }
    }

    private void Fire()
    {
        Projectile projectile = Instantiate(projectilePrefab, position,
            Quaternion.identity).GetComponent<Projectile>();
        projectile.target = targets.Peek();
        projectile.transform.rotation = Quaternion.LookRotation(Vector3.forward, targets.Peek().position - projectile.transform.position);
        projectile.transform.Rotate(Vector3.forward, 90);
        projectile.damage = damage;
        typeManager.SetType(type, projectile);
    }
}
