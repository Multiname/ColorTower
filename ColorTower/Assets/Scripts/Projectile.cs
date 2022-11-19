using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public Animator animator;

    public float speed = 5.0f;

    [HideInInspector]
    public TypeManager.Type type;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public int damage;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
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
            Destroy(gameObject);
            if (!collision.gameObject.TryGetComponent(out Enemy enemy))
                return;
            enemy.TakeDamage(damage, type);
        }
    }
}
