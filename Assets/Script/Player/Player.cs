using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] int money = 5;
    [SerializeField] int GambleMoney = 1;
    [SerializeField] SelectEvent selectEvent;
    [SerializeField] SelectEvent OnWin;
    [SerializeField] SelectEvent OnLose;
    [SerializeField] Gamble gamble;
    [SerializeField] Button reset;
    [SerializeField] AudioManager audioManager;

    public int items;

    public int Money
    {
        get => money;
        set
        {
            money = value;
            ResetMoney(value);
        }
    }
    [Header("YOUNG MONEY BABY")]
    [SerializeField] Sprite coin0;
    [SerializeField] Sprite coin1;
    [SerializeField] Sprite coin2;
    [SerializeField] Sprite coin3;
    [SerializeField] Sprite coin4;
    [SerializeField] Sprite coin5;
    [SerializeField] Sprite coin6;
    [SerializeField] Sprite coin7;
    [SerializeField] Sprite coin10;
    [SerializeField] Sprite coin20;
    [SerializeField] Sprite coin40;
    [SerializeField] Sprite coin100;
    [SerializeField] Image MoneyImg;

    private void ResetMoney(int money)
    {

        if (money < 1)
        {
            MoneyImg.sprite = coin0;
        }

        else if (money == 1)
        {
            MoneyImg.sprite = coin1;
        }
        else if (money == 2)
        {
            MoneyImg.sprite = coin2;
        }
        else if (money == 3)
        {
            MoneyImg.sprite = coin3;
        }
        else if (money == 4)
        {
            MoneyImg.sprite = coin4;
        }
        else if (money == 5)
        {
            MoneyImg.sprite = coin5;
        }
        else if (money == 6)
        {
            MoneyImg.sprite = coin6;
        }
        else if (money == 7)
        {
            MoneyImg.sprite = coin7;
        }
        else if (money > 7 && money < 10)
        {
            MoneyImg.sprite = coin10;
        }
        else if (money >= 10 && money < 20)
        {
            MoneyImg.sprite = coin20;
        }
        else if (money >= 20 && money <= 40)
        {
            MoneyImg.sprite = coin40;
        }
        else if (money > 40)
        {
            MoneyImg.sprite = coin100;
        }
    }
    private void OnEnable()
    {
        selectEvent.Register(SetNewGamble);
        OnWin.Register(GetMoney);
        // OnLose.Register(GetMoney);
    }

    private void OnDisable()
    {
        selectEvent.Unregister(SetNewGamble);
        OnWin.Unregister(GetMoney);
        // OnLose.Unregister(GetMoney);
    }
    private void Start()
    {
        ResetMoney(Money);
    }
    public void GetMoney(ICreatureController creatureController)
    {
        if (creatureController.IsSelect)
        {
            Money += 5;
        }
        audioManager.PlayGetMoney();

    }
    public void SetNewGamble(ICreatureController creatureController)
    {
        audioManager.PlayDual();
        GameManager gameManager = FindAnyObjectByType<GameManager>();
        gameManager.IsSelect = true;
        gamble = new();
        creatureController.IsSelect = true;
        if (Money < GambleMoney)
        {
            gameManager.ResetGame();
        }
        gamble.money = GambleMoney;
        Money -= GambleMoney;
    }
}
class Gamble
{
    public ICreatureController creatureController;
    public int money;
}
