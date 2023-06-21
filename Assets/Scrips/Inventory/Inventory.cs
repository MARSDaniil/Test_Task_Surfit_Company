using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
   

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
}
