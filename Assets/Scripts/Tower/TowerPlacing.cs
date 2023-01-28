using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.EventSystems;

public class TowerPlacing : MonoBehaviour
{
    private GridManager gridManager;
    private Rigidbody2D rb;

    public bool isOnPlacedGrid;

    private EnemyWaveManager target;
    Vector2 moveDirection;
    float targetDistance = 3f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gridManager = FindObjectOfType<GridManager>();
        target = FindObjectOfType<EnemyWaveManager>();
    }

    private void Update()
    {
        if (!isOnPlacedGrid) {
            transform.position = gridManager.smoothmousePosition + new Vector3(0, 0f, -4f);
        }

        if (Vector3.Distance(transform.position, target.enemyInstance.transform.position) < targetDistance)
        {
            moveDirection = (target.enemyInstance.transform.position - transform.position).normalized;

            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(-50, 180, -angle - 90);
        }
    }
}
