using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerPlacing : MonoBehaviour
{
    private GridManager gridManager;
    EnemyWaveManager enemyWaveManager;

    public bool isOnPlacedGrid;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        enemyWaveManager = FindObjectOfType<EnemyWaveManager>();
    }

    private void Update()
    {
        if (!isOnPlacedGrid)
        {
            transform.position = gridManager.smoothmousePosition + new Vector3(0, 0f, -4f);
        }

        enemyWaveManager.TowerLookAtEnemy();
    }
}
