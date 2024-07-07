using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
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

		[SerializeField]
		private ItemActionPanel actionPanel;

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
				item.OnRightMouseBtnClick += HandleShowItemActions;
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

		public void UpdateData(int itemIndex,
				Sprite itemImage, int itemQuantity)
		{
			if (listOfItems.Count > itemIndex)
			{
				listOfItems[itemIndex].SetData(itemImage, itemQuantity);
			}
		}

		private void HandleShowItemActions(UIInventoryItem item)
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
			ResetDraggedItem();
		}

		private void HandleItemBeginDrag(UIInventoryItem item)
		{
			int index = listOfItems.IndexOf(item);
			if (index == -1)
			{
				return;
			}
			current = index;
			HandleItemSelection(item);
			OnStartDragging?.Invoke(index);
		}

		public void CreateDraggedItem(Sprite sprite, int quantity)
		{
			mouseFollower.Toggle(true);
			mouseFollower.SetData(sprite, quantity);
		}


		private void HandleSwap(UIInventoryItem item)
		{
			int index = listOfItems.IndexOf(item);
			if (index == -1)
			{
				return;
			}
			OnSwapItems?.Invoke(current, index);
			HandleItemSelection(item);


		}
		private void ResetDraggedItem()
		{
			mouseFollower.Toggle(false);
			current = -1;
		}

		private void HandleItemSelection(UIInventoryItem item)
		{
			int index = listOfItems.IndexOf(item);
			if (index == -1)
			{
				return;
			}
			OnDescriptionRequested?.Invoke(index);
		}

		public void Show()
		{
			gameObject.SetActive(true);
			ResetSelection();
		}

		public void ResetSelection()
		{
			itemDescription.ResetDescription();
			DeselectAllItems();
		}

		private void DeselectAllItems()
		{
			foreach (UIInventoryItem item in listOfItems)
			{
				item.Deselect();
			}
			actionPanel.Toggle(false);
		}


		public void Hide()
		{
			actionPanel.Toggle(false);
			gameObject.SetActive(false);
			ResetDraggedItem();
		}

		internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
		{
			itemDescription.SetDescription(itemImage, name, description);
			DeselectAllItems();
			listOfItems[itemIndex].Select();
		}

		public void AddAction(string actionName, Action performAction)
		{
			actionPanel.AddButon(actionName, performAction);
		}

		public void ShowItemAction(int itemIndex)
		{
			actionPanel.Toggle(true);
			actionPanel.transform.position = listOfItems[itemIndex].transform.position;
		}
	}
}