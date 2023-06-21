using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Item : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
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

    /*
     
     
    private readonly float smallSizeSqure = 1f; 
    private readonly float MediuimHorizontal = 1f;
    private readonly float MediumVertical = 3f;
    private readonly float MediumSquare = 2;
    private readonly float LargeHorizontal = 2f;
    private readonly float LargeVertical = 5f;  
     */

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
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }
    public void OnDrag(PointerEventData eventData) // follow to arrow
    {
        positionItem = Input.mousePosition;
        positionItem.x -= Screen.width/2;
        positionItem.y -= Screen.height/2;
        rectTransform.anchoredPosition = positionItem;
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
}
