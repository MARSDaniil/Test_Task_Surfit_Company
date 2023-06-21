using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Cell : MonoBehaviour, IDropHandler
{
    public TMP_Text CellIndex;
    public TMP_Text CellText;




    private float xCoorinate;
    private float yCoorinate;
    private bool isFree;
    public Inventory inventory;
    
    private void Start()
    {



    }
    
    public void ChangeCoordinateX(float newCoordinate)
    {
        ChangeCoordinate(xCoorinate, newCoordinate);
    }

    public void ChangeCoordinateY(float newCoordinate)
    {
        ChangeCoordinate(yCoorinate, newCoordinate);
    }

    private void ChangeCoordinate(float oldCoordinate, float newCoordionate)
    {
        oldCoordinate = newCoordionate;
    }

    public void IsFreeChange(bool newFree)
    {
        isFree = newFree;
    }

  

    public void OnDrop(PointerEventData eventData)
    {
        var dragItem = eventData.pointerDrag.GetComponent<Item>();
        if(isFree)
        {
            dragItem.transform.SetParent(transform); //keeping track of cell position in canvas global coordinates
            dragItem.transform.localPosition = Vector3.zero;//change local position to Cell and null start coords
            
            var itemSize = dragItem.GetSize();
            var newPos = dragItem.transform.localPosition;


            if(itemSize.x > 1)
            {
                
                float spacingBetweenItems = (itemSize.x - 1) * inventory.layoutGroup.spacing.x/2;

                newPos.x += ((itemSize.x-1) * inventory.layoutGroup.cellSize.x/2 + spacingBetweenItems);
               // Debug.Log("newPos.x" + itemSize.x * inventory.layoutGroupCellSizeX/2);
            }
            if(itemSize.y > 1)
            {
                float spacingBetweenItems = (itemSize.y - 1)*inventory.layoutGroup.spacing.y/2;

                Debug.Log("itemSize.y = " + itemSize.y);

                newPos.y -= ((itemSize.y - 1) * inventory.layoutGroup.cellSize.y/2 + spacingBetweenItems);

            }
            dragItem.transform.localPosition = newPos;
            dragItem.transform.SetParent(inventory.transform);//cancel set to parent
            

        }
    }
}
