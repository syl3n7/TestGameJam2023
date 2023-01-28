using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public GameObject enemyObject;
    private List<Vector2Int> pathRoute;
    public GameObject enemyInstance;
    int nextPathCellIndex;
    bool enemyRunCompleted;

    private void Start()
    {
        InstanciateEnemy();
        nextPathCellIndex = 1;
        enemyRunCompleted = false;
    }

    private void Update()
    {
        if (pathRoute != null && pathRoute.Count > 1 && !enemyRunCompleted)
        {
            Vector3 currentPos = enemyInstance.transform.position;
            Vector3 nextPos = new Vector3(pathRoute[nextPathCellIndex].x, pathRoute[nextPathCellIndex].y, -1f);
            enemyInstance.transform.position = Vector3.MoveTowards(currentPos, nextPos, 4 * Time.deltaTime);

            if (Vector3.Distance(currentPos, nextPos) < .05f)
            {
                if (nextPathCellIndex >= pathRoute.Count - 1)
                {
                    Debug.Log("123Boom");
                    enemyRunCompleted = true;
                    InstanciateEnemy();
                }
                else
                {
                    nextPathCellIndex++;
                }
            }
        }
    }

    public void InstanciateEnemy()
    {
        enemyInstance = Instantiate(enemyObject, new Vector3(.2f, 6f, 0f), Quaternion.identity);
        enemyRunCompleted = false;
        nextPathCellIndex = 0;
        enemyInstance.transform.eulerAngles = new Vector3(-50f, 180f, -90f);
    }

    public void SetPathCells(List<Vector2Int> pathCells)
    {
        this.pathRoute = pathCells;
    }
}
