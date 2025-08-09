using System.Collections.Generic;
using UnityEngine;
/// Container to store where the element is located in the grid
[CreateAssetMenu(fileName = "new GridSpawnData", menuName = "Grid/Grid Spawn Data")]
public class GridSpawnData : ScriptableObject
{
    // A 10x10 grid represented as a flat list of cell types
    public List<CellType> grid;

    private void OnEnable()
    {
        if (grid == null || grid.Count != 100)
        {
            grid = new List<CellType>(new CellType[100]);
        }
    }
/// Resets all grid cells to EmptyCell.
    public void ResetGrid()
    {
        for (int i = 0; i < grid.Count; i++)
        {
            grid[i] = CellType.EmptyCell;
        }
    }
}
