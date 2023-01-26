using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveVelocityS : MonoBehaviour, IMoveVelocityS
{
    [SerializeField] private float moveSpeed;

    private Vector3 velocityVector;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(Vector3 velocityVector)
    {
        this.velocityVector = velocityVector;
    }

    private void FixedUpdate()
    {
        rb2D.velocity = velocityVector * moveSpeed;
    }
}
