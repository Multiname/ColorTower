using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;

    private TypeManager typeManager;

    [SerializeField]
    private float cooldown = 1.0f;

    [HideInInspector]
    public TypeManager.Type originalType;
    [HideInInspector]
    public TypeManager.Type currentType;
    [HideInInspector]
    public int damage = 1;
    [HideInInspector]
    public CircleCollider2D rangeCollider;

    private Vector3 position;
    private Queue<Transform> targets = new();

    private void Awake()
    {
        typeManager = GameObject.FindWithTag("TypeManager").GetComponent<TypeManager>();
        
        position = transform.parent.position;
        position.z = -1;
        rangeCollider = GetComponent<CircleCollider2D>();
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
        projectile.damage = damage;
        typeManager.SetType(currentType, projectile);
    }
}
