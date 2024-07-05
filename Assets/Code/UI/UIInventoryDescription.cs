using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Inventory.UI
{
	public class UIInventoryDescription : MonoBehaviour
	{
		[SerializeField]
		private Image itemImage;
		[SerializeField]
		private TMP_Text title;
		[SerializeField]
		private TMP_Text description;

		public void Awake()
		{
			ResetDescription();
		}

		public void ResetDescription()
		{
			itemImage.gameObject.SetActive(false);
			title.text = "";
			description.text = "";
		}

		public void SetDescription(Sprite itemSprite, string itemTitle, string itemDescription)
		{
			itemImage.sprite = itemSprite;
			itemImage.gameObject.SetActive(true);
			title.text = itemTitle;
			description.text = itemDescription;
		}
	}
}