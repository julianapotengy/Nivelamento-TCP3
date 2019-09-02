using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    int money;
    float timer, initialTimer;

    Text moneyTxt;
    GameObject storePanel;

    void Awake()
    {
        money = 0;
        initialTimer = 1.5f;
        timer = initialTimer;
        moneyTxt = GameObject.Find("MoneyText").GetComponent<Text>();
        storePanel = GameObject.Find("StorePanel");
        storePanel.SetActive(false);
    }

    void Update()
    {
        AddMoneyWithTime();

        moneyTxt.text = "Dinheiro: " + GetMoney().ToString();

        if (Input.GetKeyDown(InputManager.IM.store))
        {
            if(storePanel.activeSelf)
            {
                storePanel.SetActive(false);
            }
            else storePanel.SetActive(true);
        }
    }

    void AddMoneyWithTime()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            money += 1;
            timer = initialTimer;
        }
    }

    public int GetMoney()
    {
        return money;
    }

    public void ReduceMoney(int price)
    {
        money -= price;
    }
}
