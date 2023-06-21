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
        for (int i = 0; i < SizeY; i++)
        {
            for (int j = 0; j < SizeX; j++)
            {
                var newCell = Instantiate(cellPrefab, transformTransform);
                newCell.name = "cell" + i + j;
                newCell.ChangeCoordinateX(j);
                newCell.ChangeCoordinateY(i);
                newCell.IsFreeChange(true);
                newCell.inventory = this;
                newCell.CellIndex.text = i + " " + j;
                cells[i, j] = newCell;
            }
        }
    }
}
