using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator
{
    private int width, height;
    public List<Vector2Int> pathCells;
    private List<Vector2Int> route;

    public PathGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public List<Vector2Int> GeneratePath()
    {
        pathCells = new List<Vector2Int>();

        int y = (int)(height / 2);
        int x = 0;

        /*
        for (int x = 0; x < width; x++)
        {
            pathCells.Add(new Vector2Int(x, y));
        }*/

        while (x < width)
        {
            pathCells.Add(new Vector2Int(x , y ));

            bool validMove = false;

            while (!validMove)
            {
                int move = Random.Range(0, 3);

                if (move == 0 || x % 2 == 0 || x > (width - 2))
                {
                    x++;
                    validMove = true;
                }
                else if (move == 1 && CellIsEmpty(x, y + 1) && y < (height - 2))
                {
                    y++;
                    validMove = true;
                }
                else if (move == 2 && CellIsEmpty(x, y - 1) && y > 2)
                {
                    y--;
                    validMove = true;
                }
            }
        }

        return pathCells;
    }

    public List<Vector2Int> GenerateRoute()
    {
        Vector2Int direction = Vector2Int.right;
        route = new List<Vector2Int>();
        Vector2Int currentCell = pathCells[0];

        while (currentCell.x < 23)
        {
            route.Add(new Vector2Int(currentCell.x, currentCell.y));

            if (CellIsTaken(currentCell + direction))
            {
                currentCell = currentCell + direction;

            }
            else if (CellIsTaken(currentCell + Vector2Int.up) && direction != Vector2Int.down)
            {
                direction = Vector2Int.up;
                currentCell = currentCell + direction;
            }
            else if (CellIsTaken(currentCell + Vector2Int.down) && direction != Vector2Int.up)
            {
                direction = Vector2Int.down;
                currentCell = currentCell + direction;
            }
            else if (CellIsTaken(currentCell + Vector2Int.right) && direction != Vector2Int.left)
            {
                direction = Vector2Int.right;
                currentCell = currentCell + direction;
            }
            else if (CellIsTaken(currentCell + Vector2Int.left) && direction != Vector2Int.right)
            {
                direction = Vector2Int.left;
                currentCell = currentCell + direction;
            }
            else
            {
                throw new System.Exception("How=?==?");
            }
        }

        return route;
    }

    public bool CellIsEmpty(int x, int y)
    {
        return !pathCells.Contains(new Vector2Int(x, y));
    }

    public bool CellIsTaken(int x, int y)
    {
        return pathCells.Contains(new Vector2Int(x, y));
    }

    public bool CellIsTaken(Vector2Int cell)
    {
        return pathCells.Contains(cell);
    }

    public int GetCellNeighbourValue(int x, int y)
    {
        int returnValue = 0;

        if (CellIsTaken(x, y - 1))
        {
             returnValue += 1;
        }

        if (CellIsTaken(x - 1, y))
        {
             returnValue += 2;
        }

        if (CellIsTaken(x + 1, y))
        {
             returnValue += 4;
        }

        if (CellIsTaken(x, y + 1))
        {
             returnValue += 8;
        }

        return returnValue;
    }
}
