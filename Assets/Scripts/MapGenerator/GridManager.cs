using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    #region Get Mouse Position in World with Z = 0f
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    public static Vector3 GetDirToMouse(Vector3 fromPosition)
    {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        return (mouseWorldPosition - fromPosition).normalized;
    }
    #endregion

    public int gridWidth = 16;
    public int gridHeight = 8;
    public int minPathLength = 30;
    public Vector3 smoothmousePosition;
    public Transform tower;
    public Transform onMousePrefabe;

    private EnemyWaveManager waveManager;

    public GridCellObjects[] gridCellsObjects;
    public GridCellObjects[] sceneryCellsObjects;
    public GridCellObjects[] placedCellObjects;

    private PathGenerator pathGenerator;

    private Vector2 mousePosition;
    private Node[,] nodes;

    private void Start()
    {
        pathGenerator = new PathGenerator(gridWidth, gridHeight);
        waveManager = GetComponent<EnemyWaveManager>();

        List<Vector2Int> pathCells = pathGenerator.GeneratePath();

        int pathSize = pathCells.Count;

        while (pathSize < minPathLength)
        {
            pathCells = pathGenerator.GeneratePath();
            pathSize = pathCells.Count;
        }

        StartCoroutine(CreateGrid(pathCells));
    }
    private void Update()
    {
        GetMousePositionOnGrid();
    }

    private IEnumerator CreateGrid(List<Vector2Int> pathCells)
    {
        yield return LayPathCells(pathCells);
        yield return LaySceneryCells();
        yield return LayPlacedCells();
        yield return LayEnemyCell();
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
            yield return new WaitForSeconds(.01f);
        }

        yield return null;
    }
    private IEnumerator LaySceneryCells()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (pathGenerator.CellIsEmpty(x, y) && pathGenerator.CellIsEmpty(x, y - 1) && pathGenerator.CellIsEmpty(x, y + 1) &&
                        pathGenerator.CellIsEmpty(x + 1, y) && pathGenerator.CellIsEmpty(x - 1, y))
                {
                    int randomScenery = UnityEngine.Random.Range(0, sceneryCellsObjects.Length);
                    Instantiate(sceneryCellsObjects[randomScenery].cellPrefab, new Vector2(x, y), Quaternion.identity);
                    yield return new WaitForSeconds(.01f);
                }
            }
        }

        yield return null;
    }

    private IEnumerator LayPlacedCells()
    {
        nodes = new Node[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (pathGenerator.CellIsEmpty(x, y) && pathGenerator.CellIsTaken(x + 1, y) || pathGenerator.CellIsEmpty(x, y) && pathGenerator.CellIsTaken(x, y + 1)
                    || pathGenerator.CellIsEmpty(x, y) && pathGenerator.CellIsTaken(x - 1, y) || pathGenerator.CellIsEmpty(x, y) && pathGenerator.CellIsTaken(x, y - 1))
                {
                    int randomPlaced = UnityEngine.Random.Range(0, placedCellObjects.Length);
                    GameObject obj = Instantiate(placedCellObjects[randomPlaced].cellPrefab, new Vector2(x, y), Quaternion.identity);
                    nodes[x, y] = new Node(true, new Vector2(x, y), obj);
                    yield return new WaitForSeconds(.01f);
                }
            }
        }

        yield return null;
    }

    private IEnumerator LayEnemyCell()
    {
        waveManager.SetPathCells(pathGenerator.GenerateRoute());
        yield return null;
    }

    void GetMousePositionOnGrid()
    {
        Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hitInfo = Physics2D.Raycast(ray, Vector2.zero);

        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("DropArea"))
            {
                foreach (var node in nodes)
                {
                    if (Input.GetMouseButtonUp(0) && onMousePrefabe != null)
                    {
                        //node.isPlaceable = false;
                        onMousePrefabe.eulerAngles = new Vector3(-50f, 180f, -90f);
                        onMousePrefabe.GetComponent<TowerPlacing>().isOnPlacedGrid = true;
                        //onMousePrefabe.position = node.placedCellPosition + new Vector2(0f, 0f);
                        onMousePrefabe = null;
                    }

                }
            }
        }

        mousePosition = ray;
        smoothmousePosition = mousePosition;
        mousePosition.y = 0;
        mousePosition = Vector2Int.RoundToInt(mousePosition);
    }

    public void OnMouseClickOnUI()
    {
        if (onMousePrefabe == null)
        {
            onMousePrefabe = Instantiate(tower, GetMouseWorldPosition(), Quaternion.identity); 
        }
    }

    public class Node
    {
        public bool isPlaceable;
        public Vector2 placedCellPosition;
        public GameObject obj;

        public Node(bool isPlaceable, Vector2 placedCellPosition, GameObject obj)
        {
            this.isPlaceable = isPlaceable;
            this.placedCellPosition = placedCellPosition;
            this.obj = obj;
        }
    }
}
