using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class Cell : MonoBehaviour, IDropHandler
{
    public TMP_Text CellIndex;
    public TMP_Text CellText;


    public GameObject CellPref;

    private float xCoorinate;
    private float yCoorinate;
    private bool isFree;
    public Inventory inventory;
    /*
    private void Start()
    {
        inventory = GetComponent<Inventory>();
    }
    */
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

    public void SetPrefab(GameObject gameObject)
    {
        CellPref = gameObject;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var dragItem = eventData.pointerDrag.GetComponent<Item>();
        if(isFree)
        {
            //keeping track of cell position in canvas global coordinates
            dragItem.transform.SetParent(transform);
            //change local position to Cell and null start coords
            dragItem.transform.localPosition = Vector3.zero;
            //cancel set to parent
            dragItem.transform.SetParent(inventory.transform);
        }
    }
}
