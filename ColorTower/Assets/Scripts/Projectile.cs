using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public TypeManager.Type type;
    public SpriteRenderer spriteRenderer { get; private set; }
    public float speed = 5.0f;
    public Transform target;
    public int damage;
    public Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            Destroy(gameObject);
        else
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, target.position - transform.position);
            transform.Rotate(Vector3.forward, 90);
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.transform == target)
        {
            if (!collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                Destroy(gameObject);
                return;
            }
            enemy.TakeDamage(damage, type);
            Destroy(gameObject);
        }
    }
}
