using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlacing : MonoBehaviour
{
    private GridManager gridManager;

    public bool isOnPlacedGrid;

    EnemyWaveManager enemy;

    Vector2 moveDirection;
    float targetDistance = 4f;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        enemy = FindObjectOfType<EnemyWaveManager>();
    }

    private void Update()
    {
        if (!isOnPlacedGrid)
        {
            transform.position = gridManager.smoothmousePosition + new Vector3(0, 0f, -4f);
        }

        if (Vector3.Distance(transform.position, enemy.Tower()) < targetDistance)
        {
            moveDirection = (enemy.Tower() - transform.position).normalized;

            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(-50, 180, -angle - 90);
        }
    }
}
