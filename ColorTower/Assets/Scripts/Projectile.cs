using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public TypeManager.Type type;
    public SpriteRenderer SpriteRenderer { get; private set; }
    public float speed = 5.0f;
    public Transform target;
    public int damage;

    // Start is called before the first frame update
    void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            Destroy(gameObject);
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, target.position - transform.position);
            transform.Rotate(Vector3.forward, 90);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
