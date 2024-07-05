using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Inventory
{
	public class InventoryController : MonoBehaviour
	{
		[SerializeField]
		private UIInventoryPage inventoryPage;
		[SerializeField]
		private InventorySO inventoryData;

		public List<InventoryItem> initialItems = new List<InventoryItem>();

		[SerializeField]
		private AudioClip dropClip;

		[SerializeField]
		private AudioSource audioSource;

		public void Start()
		{
			PrepareUI();
			PrepareInventoryData();
		}
		private void PrepareInventoryData()
		{
			inventoryData.Initialize();
			inventoryData.OnInventoryUpdated += UpdateInventoryUI;
			foreach (InventoryItem item in initialItems)
			{
				if (item.IsEmpty)
					continue;
				inventoryData.AddItem(item);
			}
		}
		private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
		{
			inventoryPage.ResetAllItems();
			foreach (var item in inventoryState)
			{
				inventoryPage.UpdateData(item.Key, item.Value.item.ItemImage,
					item.Value.quantity);
			}
		}
		private void PrepareUI()
		{
			inventoryPage.InitializeInventoryUI(inventoryData.Size);
			inventoryPage.OnDescriptionRequested += HandleDescriptionRequest;
			inventoryPage.OnSwapItems += HandleSwapItems;
			inventoryPage.OnStartDragging += HandleDragging;
			inventoryPage.OnItemActionRequested += HandleItemActionRequest;
		}

		private void HandleItemActionRequest(int itemIndex)
		{
			InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
			if (inventoryItem.IsEmpty)
				return;

			IItemAction itemAction = inventoryItem.item as IItemAction;
			if (itemAction != null)
			{

				inventoryPage.ShowItemAction(itemIndex);
				inventoryPage.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
			}

			IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
			if (destroyableItem != null)
			{
				inventoryPage.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
			}

		}

		private void HandleDragging(int itemIndex)
		{
			InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
			if (inventoryItem.IsEmpty)
				return;
			inventoryPage.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
		}

		private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
		{
			inventoryData.SwapItems(itemIndex_1, itemIndex_2);
		}

		private void HandleDescriptionRequest(int itemIndex)
		{
			InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
			if (inventoryItem.IsEmpty)
			{
				inventoryPage.ResetSelection();
				return;
			}
			ItemSO item = inventoryItem.item;
			string description = PrepareDescription(inventoryItem);
			inventoryPage.UpdateDescription(itemIndex, item.ItemImage,
				item.name, description);
		}

		private string PrepareDescription(InventoryItem inventoryItem)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(inventoryItem.item.Description);
			sb.AppendLine();
			for (int i = 0; i < inventoryItem.itemState.Count; i++)
			{
				sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName} " +
					$": {inventoryItem.itemState[i].value} / " +
					$"{inventoryItem.item.DefaultParametersList[i].value}");
				sb.AppendLine();
			}
			return sb.ToString();
		}


		public void Update()
		{
			if (Input.GetKeyDown(KeyCode.I))
			{
				if (inventoryPage.isActiveAndEnabled == false)
				{
					inventoryPage.Show();
					foreach (var item in inventoryData.GetCurrentInventoryState())
					{
						inventoryPage.UpdateData(item.Key,
							item.Value.item.ItemImage,
							item.Value.quantity);
					}
				}
				else
				{
					inventoryPage.Hide();
				}
			}
		}

		private void DropItem(int itemIndex, int quantity)
		{
			inventoryData.RemoveItem(itemIndex, quantity);
			inventoryPage.ResetSelection();
			audioSource.PlayOneShot(dropClip);
		}

		public void PerformAction(int itemIndex)
		{
			InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
			if (inventoryItem.IsEmpty)
				return;

			IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
			if (destroyableItem != null)
			{
				inventoryData.RemoveItem(itemIndex, 1);
			}

			IItemAction itemAction = inventoryItem.item as IItemAction;
			if (itemAction != null)
			{
				itemAction.PerformAction(gameObject, inventoryItem.itemState);
				audioSource.PlayOneShot(itemAction.actionSFX);
				if (inventoryData.GetItemAt(itemIndex).IsEmpty)
					inventoryPage.ResetSelection();
			}
		}
	}
}