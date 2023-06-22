using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
public class Cell : MonoBehaviour, IDropHandler, IPointerEnterHandler,IPointerExitHandler
{
    public TMP_Text CellIndex;
    public TMP_Text CellText;

    

    private int xCoorinate;
    private int yCoorinate;

    [SerializeField]
    private bool isFree;
    
    public Inventory inventory;

    public Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (!isFree)
        {
            image.color = Color.red;
        }
    }

    public void ChangeCoordinateX(int newCoordinate)
    {
        xCoorinate = newCoordinate;
    }
    public void ChangeCoordinateY(int newCoordinate)
    {
        yCoorinate = newCoordinate;
    }

    public int GetCoordinateX()
    {
        return xCoorinate;
    }
    public int GetCoordinateY()
    {
        return yCoorinate;
    }

    public void IsFreeChange(bool newFree)
    {
        isFree = newFree;
    }
    public bool IsFreeCheck()
    {
        if (isFree == true)
        {
            return true;
        }
        else
            return false;
    }

    public void ShowIndex()
    {
        Debug.Log("x = " + xCoorinate + ", y = " + yCoorinate);
    }

    public void OnDrop(PointerEventData eventData) //drop Object to Cell
    {
        var dragItem = eventData.pointerDrag.GetComponent<Item>();
        if (inventory.CheckCellFree(this, dragItem.itemSize))
        {
            dragItem.SetPosition(dragItem, this);
            dragItem.PrevCell = this; //assigning a cell when hitting a cell
        }
        else
        {
            dragItem.SetPosition(dragItem, dragItem.PrevCell);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       if(inventory.draggenItemPref )
       {
            if (isFree)
            {
                inventory.CellsColorize(this, inventory.draggenItemPref.itemSize, Color.green);
            }
          
       }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(isFree)
        {
            inventory.CellsColorize(this, inventory.draggenItemPref.itemSize, Color.white);
        }
    }

}
