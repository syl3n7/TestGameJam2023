using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public Wave[] waveArray;

    private void Start()
    {
        
    }

    private void Update()
    {
        foreach (Wave wave in waveArray)
        {
            wave.Update();
        }
    }

    public void SetPathCells(List<Vector2Int> pathCells)
    {
        foreach (Wave wave in waveArray)
        {
            wave.SetPathCells(pathCells);
        }
    }

    public Vector3 Tower()
    {
        Vector3 enemy;
        enemy = waveArray[0].enemyInstance.transform.position;
        return enemy;
    }

    [System.Serializable]
    public class Wave
    {
        public GameObject[] enemySpawnArray;
        public GameObject enemyInstance;
        public List<Vector2Int> pathRoute;
        public int nextPathCellIndex;
        public bool enemyRunCompleted;
        public float timer;

        public void Start()
        {
            SpawnEnemies();
            nextPathCellIndex = 1;
        }

        public void Update()
        {
            EnemiesPath();

            if (timer >= 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    SpawnEnemies();
                }
            }
        }

        public void SpawnEnemies()
        {
            foreach (GameObject enemySpawn in enemySpawnArray)
            {
                enemyInstance = Instantiate(enemySpawn, new Vector3(.2f, 6f, 0f), Quaternion.identity);
                enemyInstance.transform.eulerAngles = new Vector3(-50f, 180f, -90f);
                nextPathCellIndex = 0;
            }
        }
        public void EnemiesPath()
        {
            if (pathRoute != null && pathRoute.Count > 1)
            {
                Vector3 currentPos = enemyInstance.transform.position;
                Vector3 nextPos = new Vector3(pathRoute[nextPathCellIndex].x, pathRoute[nextPathCellIndex].y, -1f);
                enemyInstance.transform.position = Vector3.MoveTowards(currentPos, nextPos, 4 * Time.deltaTime);

                if (Vector3.Distance(currentPos, nextPos) < .05f)
                {
                    if (nextPathCellIndex >= pathRoute.Count - 1)
                    {
                        SpawnEnemies();
                    }
                    else
                    {
                        nextPathCellIndex++;
                    }
                }
            }
        }

        public void SetPathCells(List<Vector2Int> pathCells)
        {
            this.pathRoute = pathCells;
        }
    }
}
