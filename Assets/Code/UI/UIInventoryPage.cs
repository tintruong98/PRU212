using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab;
    [SerializeField]
    private RectTransform itemContainer;
    [SerializeField]
    private UIInventoryDescription itemDescription;
    [SerializeField]
    private MouseFollower mouseFollower;

    List<UIInventoryItem> listOfItems = new List<UIInventoryItem>();

    private int current = -1;

    public event Action<int> OnDescriptionRequested, 
        OnItemActionRequested, OnStartDragging;

    public event Action<int, int> OnSwapItems;

    private void Awake()
    {
        Hide();
        mouseFollower.Toggle(false);
        itemDescription.ResetDescription();
    }

    public void InitializeInventoryUI(int inventorysize)
    {
        for (int i = 0; i < inventorysize; i++)
        {
            UIInventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            item.transform.SetParent(itemContainer);    
            listOfItems.Add(item);
            item.OnItemClicked += HandleItemSelection;
            item.OnItemBeginDrag += HandleItemBeginDrag;
            item.OnItemDroppedOn += HandleSwap;
            item.OnItemEndDrag += HandleItemEndDrag;
            item.OnRightMouseBtnClick += HandleRightMouseBtnClick;
        }
    }

    internal void ResetAllItems()
    {
        foreach (var item in listOfItems)
        {
            item.ResetData();
            item.Deselect();
        }
    }

    //private void ResetDraggedItem()
    //{
    //    mouseFollower.Toggle(false);
    //    currentlyDraggedItemIndex = -1;
    //}

    private void HandleRightMouseBtnClick(UIInventoryItem item)
    {
        int index = listOfItems.IndexOf(item);
        if (index == -1)
        {
            return;
        }
        //OnItemActionRequested?.Invoke(index);
    }

    private void HandleItemEndDrag(UIInventoryItem item)
    {
        mouseFollower.Toggle(false);
    }

    private void HandleItemBeginDrag(UIInventoryItem item)
    {
        mouseFollower.Toggle(true);
        mouseFollower.SetData(image, quantity);
    }

    private void HandleSwap(UIInventoryItem obj)
    {
        int index = listOfItems.IndexOf(obj);
        if (index == -1)
        {
            return;
        }
        //OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
        HandleItemSelection(obj);
    }

    private void HandleItemSelection(UIInventoryItem item)
    {
        itemDescription.SetDescription(image, title, description);
        listOfItems[0].Select();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        listOfItems[0].SetData(image, quantity);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
