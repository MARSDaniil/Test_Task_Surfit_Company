using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    private readonly int smallSizeSqure = 1;
    private readonly int MediuimHorizontal = 1;
    private readonly int MediumVertical = 3;
    private readonly int MediumSquare = 2;
    private readonly int LargeHorizontal = 2;
    private readonly int LargeVertical = 5;

    public ItemSize itemSize;

    [SerializeField] private Transform transformTransform;
    public int SizeX, SizeY;
    public Cell cellPrefab;
    public Cell[,] cells;

    public GridLayoutGroup layoutGroup;

    public Item draggenItemPref;
    // Start is called before the first frame update
    void Start()
    {
        cells = new Cell[SizeX, SizeY];
        CreateNewInventory();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateNewInventory()
    {
        for (int y = 0; y < SizeY; y++)
        {
            for (int x = 0; x < SizeX; x++)
            {
                var newCell = Instantiate(cellPrefab, transformTransform);
                newCell.name = "cell" + x + y;
                newCell.ChangeCoordinateX(x);
                newCell.ChangeCoordinateY(y);
                newCell.IsFreeChange(true);
                newCell.inventory = this;
                newCell.CellIndex.text = x + " " + y;
                cells[x, y] = newCell;
            }
        }
    }

    public bool CheckCellFree(Cell cell, ItemSize size)
    {
        Vector2Int newSize = Getize(size);
        for (int y = cell.GetCoordinateY(); y < newSize.y + cell.GetCoordinateY(); y++)
        {
            for (int x = cell.GetCoordinateX(); x < newSize.x + cell.GetCoordinateX(); x++)
            {
                if (x + 1<= SizeX && y + 1<= SizeY)
                {
                    if(!cells[x,y].IsFreeCheck())
                    {
                        return false;
                    }
                }
                if(x + 1 > SizeX || y + 1  > SizeY)
                {
                    return false;
                }
            }
        }//checking for occupancy of adjacent cells

        return true;
    }

    public void CellsColorize(Cell cell, ItemSize size, Color color)
    {
        Vector2Int newSize = Getize(size);
        for (int y = cell.GetCoordinateY(); y < newSize.y + cell.GetCoordinateY(); y++)
        {
            for (int x = cell.GetCoordinateX(); x < newSize.x + cell.GetCoordinateX(); x++)
            {
                if (x + 1 <= SizeX && y + 1 <= SizeY)
                {
                    cells[x, y].image.color = color;
                }
                
            }
        }

       
    }
    public void CellsOccupation(Cell cell, ItemSize size, bool ordered)
    {
        Vector2Int newSize = Getize(size);
        for (int y = cell.GetCoordinateY(); y < cell.GetCoordinateY() + newSize.y; y++)
        {
            for (int x = cell.GetCoordinateX(); x < cell.GetCoordinateX() + newSize.x; x++)
            {
                cells[x, y].IsFreeChange(ordered);
                if(ordered)
                {
                    cells[x, y].image.color = Color.white;
                }
                else
                {
                    cells[x, y].image.color = Color.red;
                }
            }
        }
    }

    public void UpdateCellColor()
    {
        for (int y = 0; y < SizeY; y++)
        {
            for (int x = 0; x < SizeX; x++)
            {
                if(cells[x,y].IsFreeCheck())
                {
                    cells[x, y].image.color = Color.white;
                }
            }
        }
    }
    public Vector2Int Getize(ItemSize itemSize)
    {
        Vector2Int size;
        switch (itemSize)
        {
            case ItemSize.Small:
                return size = new Vector2Int(smallSizeSqure, smallSizeSqure);
            case ItemSize.MediuimHorizontal:
                return size = new Vector2Int(MediumVertical, MediuimHorizontal);
            case ItemSize.MediumVertical:
                return size = new Vector2Int(MediuimHorizontal, MediumVertical);
            case ItemSize.MediumSquare:
                return size = new Vector2Int(MediumSquare, MediumSquare);
            case ItemSize.LargeHorizontal:
                return size = new Vector2Int(LargeVertical, LargeHorizontal);
            case ItemSize.LargeVertical:
                return size = new Vector2Int(LargeHorizontal, LargeVertical);
        }
        return size = Vector2Int.zero;
    }
}
