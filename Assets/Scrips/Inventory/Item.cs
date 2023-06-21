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

   

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

    }

    // Update is called once per frame
 

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.7f;
        canvasGroup.blocksRaycasts = false;
        inventory.draggenItemPref = this;
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
        dragItem.transform.SetParent(dragItem.PrevCell.transform);
        dragItem.transform.localPosition = Vector3.zero;

        var itemSize = dragItem.GetSize();
        var newPos = dragItem.transform.localPosition;
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
        dragItem.transform.localPosition = newPos;
        dragItem.transform.SetParent(inventory.transform);//cancel set to parent

    }
}
