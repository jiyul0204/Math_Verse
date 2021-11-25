using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetDragMessage : MonoBehaviour
{
    public Transform InvisialbleCard;
    List<Arrager> arragers;

    Arrager workingArranger;
    int oriIndex;



    void Start()
    {
        arragers = new List<Arrager>();

        var arrs = transform.GetComponentsInChildren<Arrager>();

        for(int i=0;i<arrs.Length;i++)
        {
            arragers.Add(arrs[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SwapCards(Transform sour, Transform dest )
    {
        Transform sourParent = sour.parent;
        Transform destParent = dest.parent;

        int sourIndex = sour.GetSiblingIndex();
        int destIndex = dest.GetSiblingIndex();

        sour.SetParent(destParent);
        sour.SetSiblingIndex(destIndex);

        dest.SetParent(sourParent);
        dest.SetSiblingIndex(sourIndex);
    }

    void SwapCardsInHierarchy(Transform sour, Transform dest )
    {
        SwapCards(sour, dest);

        arragers.ForEach(t=>t.UpdateChildren());
    }

    bool ContainPos(RectTransform rt, Vector2 pos)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rt, pos);
    }

    void BeginDrag(Transform card)
    {
        //Debug.Log("BeginDrag : " + card.name);
        workingArranger = arragers.Find(t => ContainPos(t.transform as RectTransform, card.position));
        oriIndex = card.GetSiblingIndex();

        SwapCardsInHierarchy(InvisialbleCard, card);
    }
    void Drag(Transform card)
    {
        var whichArrangeCard = arragers.Find(t => ContainPos(t.transform as RectTransform, card.position));

        if (whichArrangeCard == null)
        {
            bool updateChildren = transform != InvisialbleCard.parent;
            InvisialbleCard.SetParent(transform);

            if(updateChildren)
            {
                arragers.ForEach(t => t.UpdateChildren());
            }
        }
        else
        {
            bool insert = InvisialbleCard.parent==transform;
            if(insert)
            {
                int index = whichArrangeCard.GetIndexByPosition(card);

                InvisialbleCard.SetParent(whichArrangeCard.transform);
                whichArrangeCard.InsertCard(InvisialbleCard, index);
            }
            int invisiableCardIndex = InvisialbleCard.GetSiblingIndex();
            int targetIndex = whichArrangeCard.GetIndexByPosition(card, invisiableCardIndex);
            if (invisiableCardIndex != targetIndex)
            {
                whichArrangeCard.SwapCard(invisiableCardIndex, targetIndex);
            }
        }

    }
    void EndDrag(Transform card)
    {
        //Debug.Log("EndDrag : " + card.name);
        if (InvisialbleCard.parent == transform)
        {
            workingArranger.InsertCard(card, oriIndex);
            workingArranger = null;
            oriIndex = -1;
        }
        else
        {
            SwapCardsInHierarchy(InvisialbleCard, card);
        }
    }
}
