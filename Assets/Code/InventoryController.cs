using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryPage;

    public int inventorySize = 10;

    public void Start()
    {
        inventoryPage.InitializeInventoryUI(inventorySize);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryPage.isActiveAndEnabled == false)
            {
                inventoryPage.Show();
            }
            else
            {
                 inventoryPage.Hide();
            }
        }
    }
}
