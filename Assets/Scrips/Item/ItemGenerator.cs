using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{

    [SerializeField] List<GameObject> SmallGameObject;
    [SerializeField] List<GameObject> MediuimHorizontal;
    [SerializeField] List<GameObject> MediumVertical;
    [SerializeField] List<GameObject> MediumSquare;
    [SerializeField] List<GameObject> LargeHorizontal;
    [SerializeField] List<GameObject> LargeVertical;

    private List<List<GameObject>> lists;
    [SerializeField] private List<Transform> PlacesList;

    

    void Start()
    {
        lists = new List<List<GameObject>>() { SmallGameObject, MediuimHorizontal, MediumVertical, MediumSquare, LargeHorizontal, LargeVertical };
        for (int i = 0; i < PlacesList.Count; i++)
        {
            List<GameObject> newList = lists[GenerateRandomNumOfList(lists.Count)];

            Instantiate(newList[GenerateRandomNumOfList(newList.Count)],PlacesList[i].position, Quaternion.identity,PlacesList[i]);
        }
   

    }



    private int GenerateRandomNumOfList(int lenghtOfList)
    {
        System.Random random = new System.Random();
        int rndNum = random.Next(0, lenghtOfList);
        return rndNum;
    }

    
}
