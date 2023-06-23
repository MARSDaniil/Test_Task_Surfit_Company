using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Item : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    
    private Canvas canvas;
    public Inventory inventory;

    private Canvas canvasPocket;
    //public Inventory inventoryPocket;

    public Cell PrevCell;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Image image;

    public ItemSize itemSize;
    public string nameOfObject;
    public Sprite spriteOfObject;
    [SerializeField]
    private bool isItemSetToInventory;//the task says that this is a float variable, but it is definitely bool






    private readonly int smallSizeSqure = 1; 
    private readonly int MediuimHorizontal = 1;
    private readonly int MediumVertical = 3;
    private readonly int MediumSquare = 2;
    private readonly int LargeHorizontal = 2;
    private readonly int LargeVertical = 5;

    private void Awake()
    {
        GameObject inventoryGameObject = GameObject.Find("InventoryCanvas");
        canvas = inventoryGameObject.GetComponent<Canvas>();
        inventory = inventoryGameObject.GetComponent<Inventory>();

        if (inventory == null)
        {
            Debug.LogError("inventoryBag == null");
        }


        GameObject pocketGameObject = GameObject.Find("pocketCanvas");
        canvasPocket = pocketGameObject.GetComponent<Canvas>();
        /*
        inventoryPocket = pocketGameObject.GetComponent<Inventory>();
        
        if(inventoryPocket == null)
        {
            Debug.LogError("inventoryPocket == null");
        }
        */
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();


    }
    void Start()
    {
        
        spriteOfObject = image.sprite;

        nameOfObject = image.sprite.name;
        name = nameOfObject + GetSize().x + GetSize().y;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.7f;
        canvasGroup.blocksRaycasts = false;

        OnBeginDragOtherInventary(inventory);
       // OnBeginDragOtherInventary(inventoryPocket);

    }

    private void OnBeginDragOtherInventary(Inventory invent)
    {
        invent.draggenItemPref = this;
        invent.UpdateCellColor();
        if (PrevCell)
        {
            invent.CellsOccupation(PrevCell, itemSize, true);
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        EndDragItem(inventory);
    //    EndDragItem(inventoryPocket);
    }

    private void EndDragItem(Inventory invent)
    {
        invent.draggenItemPref = null;
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
        SetPosition(dragItem, dragItem.PrevCell,inventory);
       // SetPosition(dragItem, dragItem.PrevCell,inventoryPocket);

    }

    public void SetPosition(Item item, Cell cell, Inventory inventory)
    {
        if (cell && item) //protection against leaving the item outside the inventory when first placed
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
}
