using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        this.itemImage.gameObject.SetActive(false);
        this.title.text = "";
        this.description.text = "";
    }

    public void SetDescription(Sprite itemSprite, string itemTitle, string itemDescription)
    {
        this.itemImage.sprite = itemSprite;
        this.itemImage.gameObject.SetActive(true);
        this.title.text = itemTitle;
        this.description.text = itemDescription;
    }
}
