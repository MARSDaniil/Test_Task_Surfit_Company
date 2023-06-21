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
    private bool isFree;
    public Inventory inventory;

    Image image;

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


    public void IsFreeChange(bool newFree)
    {
        isFree = newFree;
    }

    public void ShowIndex()
    {
        Debug.Log("x = " + xCoorinate + ", y = " + yCoorinate);
    }

    public void OnDrop(PointerEventData eventData) //drop Object to Cell
    {
       
        if(isFree)
        {
            IsFreeChange(false);
            var dragItem = eventData.pointerDrag.GetComponent<Item>();
            dragItem.transform.SetParent(transform); //keeping track of cell position in canvas global coordinates
            dragItem.transform.localPosition = Vector3.zero;//change local position to Cell and null start coords
            
            var itemSize = dragItem.GetSize();
            var newPos = dragItem.transform.localPosition;

            if(itemSize.x > 1)
            {
                float spacingBetweenItems = (itemSize.x - 1) * inventory.layoutGroup.spacing.x/2;
                newPos.x += ((itemSize.x-1) * inventory.layoutGroup.cellSize.x/2 + spacingBetweenItems);
            }
            if(itemSize.y > 1)
            {
                float spacingBetweenItems = (itemSize.y - 1)*inventory.layoutGroup.spacing.y/2;
                newPos.y -= ((itemSize.y - 1) * inventory.layoutGroup.cellSize.y/2 + spacingBetweenItems);
            }

            dragItem.transform.localPosition = newPos;
            dragItem.transform.SetParent(inventory.transform);//cancel set to parent

            dragItem.PrevCell = this; //assigning a cell when hitting a cell
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       if(inventory.draggenItemPref )
       {
            if (isFree)
            {
                image.color = Color.green;
                deColourNextCell(Color.green, 1);
            }
          
       }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(isFree)
        {
            image.color = Color.white;
            deColourNextCell(Color.white, 1);

        }
    }

    private void deColourNextCell(Color newColor, int moreNum)
    {
        if (inventory.draggenItemPref)
        {
            var size = inventory.draggenItemPref.GetSize();
            if (size.x > moreNum)
            {
                if (xCoorinate + 1 < inventory.SizeX)
                {
                    inventory.cells[xCoorinate + 1, yCoorinate].image.color = newColor;
                }
            }

            if (size.y > moreNum)
            {
                if (yCoorinate + 1 < inventory.SizeY)
                {
                    inventory.cells[xCoorinate, yCoorinate + 1].image.color = newColor;
                }
            }

        }
    }
}
