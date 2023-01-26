using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (fileName = "GridCell", menuName = "TowerDefense/Grid Cell")]
public class GridCellObjects : ScriptableObject
{
    public enum CellType { Path, Ground, Placed}

    public CellType cellType;
    public GameObject cellPrefab;
    public int zRotation;
}
