using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    MoneyManager moneyMng;
    PlayerController player;

    public List<Image> imagesInInventoryList = new List<Image>();
    GameObject inventoryPanel;

    int itemQuantity;

    Color lifeColor, damageColor;
    int itemIndex;

    void Awake()
    {
        moneyMng = GameObject.Find("Manager").gameObject.GetComponent<MoneyManager>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();

        inventoryPanel = GameObject.Find("InventoryPanel");
        foreach (Image c in inventoryPanel.GetComponentsInChildren<Image>())
        {
            imagesInInventoryList.Add(c);
        }
        itemQuantity = 0;

        lifeColor = Color.green;
        damageColor = Color.blue;
    }

    public void NewItem(string itemName)
    {
        if (moneyMng.GetMoney() >= 10 && itemQuantity < 6)
        {
            StartCoroutine(AssignItem(itemName));
            ChangeColor();
        }
    }

    public IEnumerator AssignItem(string itemName)
    {
        switch (itemName)
        {
            case "life":
                player.AddLife(3);
                moneyMng.ReduceMoney(10);
                itemQuantity += 1;
                itemIndex = 0;
                break;

            case "damage":
                player.AddDamage(2);
                moneyMng.ReduceMoney(10);
                itemQuantity += 1;
                itemIndex = 1;
                break;
        }
        yield return null;
    }

    void ChangeColor()
    {
        if(itemIndex == 0)
        {
            imagesInInventoryList[itemQuantity].color = lifeColor;
        }
        else if(itemIndex == 1)
        {
            imagesInInventoryList[itemQuantity].color = damageColor;
        }
    }
}
