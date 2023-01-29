using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public static float GetangleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    float moveSpeed = 7f;

    Rigidbody2D rb2d;

    MoveVelocityS target;
    Vector2 moveDirection;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<MoveVelocityS>();
    }

    private void Update()
    {
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb2d.velocity = new Vector2(moveDirection.x, moveDirection.y);

        float angle = GetangleFromVectorFloat(moveDirection);
        transform.eulerAngles = new Vector3(0, 0, angle - 180);

        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerComponent))
        {
            playerComponent.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
