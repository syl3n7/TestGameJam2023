using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionD : MonoBehaviour
{
    private Vector3 movePosition;

    private IMoveVelocityS move;

    public void SetMovePosition(Vector3 movePosition)
    {
        this.movePosition = movePosition;
    }

    private void Start()
    {
        move = GetComponent<IMoveVelocityS>();
    }

    private void Update()
    {
        Vector3 moveDir = (movePosition - transform.position).normalized;
        if (Vector3.Distance(movePosition, transform.position) < .1f) moveDir = Vector3.zero;
        move.SetVelocity(moveDir);
    }
}
