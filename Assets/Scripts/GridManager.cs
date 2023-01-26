using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public int gridWidth = 16;
    public int gridHeight = 8;
    public int minPathLength = 30;

    public GridCellObjects[] gridCellsObjects;
    public GridCellObjects[] sceneryCellsObjects;
    public GridCellObjects[] placedCellObjects;

    public Canvas canvas;

    private PathGenerator pathGenerator;

    private void Start()
    {
        pathGenerator = new PathGenerator(gridWidth, gridHeight);

        List<Vector2Int> pathCells = pathGenerator.GeneratePath();

        int pathSize = pathCells.Count;

        while (pathSize < minPathLength)
        {
            pathCells = pathGenerator.GeneratePath();
            pathSize = pathCells.Count;
        }

        StartCoroutine(CreateGrid(pathCells));
    }

    private IEnumerator CreateGrid(List<Vector2Int> pathCells)
    {
        yield return LayPathCells(pathCells);
        yield return LayPlacedCells();
        yield return LaySceneryCells();
    }

    private IEnumerator LayPathCells(List<Vector2Int> pathCells)
    {
        foreach (Vector2Int pathCell in pathCells)
        {
            int neighbourValue = pathGenerator.GetCellNeighbourValue(pathCell.x, pathCell.y);
            Debug.Log("Tile " + pathCell.x + ", " + pathCell.y + " neighbour value = " + neighbourValue);
            GameObject pathTile = gridCellsObjects[neighbourValue].cellPrefab;
            GameObject pathTileCell = Instantiate(pathTile, new Vector2(pathCell.x, pathCell.y), Quaternion.identity);
            pathTileCell.transform.Rotate(0f, 0f, gridCellsObjects[neighbourValue].zRotation, Space.Self);
            pathTileCell.transform.SetParent(canvas.transform);
            //yield return new WaitForSeconds(.1f);
        }

        yield return null;
    }
    private IEnumerator LaySceneryCells()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (pathGenerator.CellIsEmpty(x, y) && pathGenerator.CellIsEmpty(x, y- 1) && pathGenerator.CellIsEmpty(x, y + 1) && 
                        pathGenerator.CellIsEmpty(x + 1, y) && pathGenerator.CellIsEmpty(x - 1, y))
                {
                    int randomScenery = UnityEngine.Random.Range(0, sceneryCellsObjects.Length);
                    Instantiate(sceneryCellsObjects[randomScenery].cellPrefab, new Vector2(x, y), Quaternion.identity);
                    //yield return new WaitForSeconds(.01f);
                }
            }
        }

        yield return null;
    }

    private IEnumerator LayPlacedCells()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (pathGenerator.CellIsEmpty(x, y))
                {
                    int randomPlaced = UnityEngine.Random.Range(0, placedCellObjects.Length);
                    Instantiate(placedCellObjects[randomPlaced].cellPrefab, new Vector2(x, y), Quaternion.identity);
                    //yield return new WaitForSeconds(.01f);
                }
            }
        }

        yield return null;
    }
}
