using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Item : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField]
    Canvas canvas;

    public Inventory inventory;
    public Cell PrevCell;
    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    Vector2 positionItem;
    public ItemSize itemSize;
    

    private readonly int smallSizeSqure = 1; 
    private readonly int MediuimHorizontal = 1;
    private readonly int MediumVertical = 3;
    private readonly int MediumSquare = 2;
    private readonly int LargeHorizontal = 2;
    private readonly int LargeVertical = 5;


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.7f;
        canvasGroup.blocksRaycasts = false;
        inventory.draggenItemPref = this;
        inventory.UpdateCellColor();
        if(PrevCell)
        {
            inventory.CellsOccupation(PrevCell, itemSize, true);
        } 
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        inventory.draggenItemPref = null;
        
    }
    public void OnDrag(PointerEventData eventData) // follow to arrow
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public Vector2 GetSize()
    {
        Vector2Int size;

        switch(itemSize)
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

    public void OnDrop(PointerEventData eventData)
    {
        
        var dragItem = eventData.pointerDrag.GetComponent<Item>();
        SetPosition(dragItem, dragItem.PrevCell);

    }

    public void SetPosition(Item item, Cell cell)
    {
        item.transform.SetParent(cell.transform);
        item.transform.localPosition = Vector3.zero;

        var itemSize = item.GetSize();
        var newPos = item.transform.localPosition;
        if (itemSize.x > 1)
        {

            float spacingBetweenItems = (itemSize.x - 1) * inventory.layoutGroup.spacing.x / 2;

            newPos.x += ((itemSize.x - 1) * inventory.layoutGroup.cellSize.x / 2 + spacingBetweenItems);
        }
        if (itemSize.y > 1)
        {
            float spacingBetweenItems = (itemSize.y - 1) * inventory.layoutGroup.spacing.y / 2;

            Debug.Log("itemSize.y = " + itemSize.y);

            newPos.y -= ((itemSize.y - 1) * inventory.layoutGroup.cellSize.y / 2 + spacingBetweenItems);

        }
        item.transform.localPosition = newPos;
        item.transform.SetParent(inventory.transform);//cancel set to parent
        inventory.CellsOccupation(cell, item.itemSize, false);
        inventory.UpdateCellColor();//when dragging an item outside the inventory border, painting over extra green
    }
}
