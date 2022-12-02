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
    public int damage = 2;
    [HideInInspector]
    public CircleCollider2D rangeCollider;

    private Vector3 position;
    private List<Transform> targets = new();
    private bool isAttacking = false;

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
            targets.Add(collision.transform);
            if (targets.Count == 1 && !isAttacking)
            {
                isAttacking = true;
                InvokeRepeating(nameof(Fire), 0, cooldown);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            targets.Remove(collision.transform);
    }

    private void Fire()
    {
        if (targets.Count == 0)
        {
            CancelInvoke(nameof(Fire));
            isAttacking = false;
            return;
        }

        Projectile projectile = Instantiate(projectilePrefab, position,
            Quaternion.identity).GetComponent<Projectile>();
        projectile.target = targets[0];
        projectile.damage = damage;
        typeManager.SetType(currentType, projectile);
    }
}
