using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] int money = 20;
    [SerializeField] int GambleMoney = 10;
    [SerializeField] SelectEvent selectEvent;
    [SerializeField] SelectEvent OnWin;
    [SerializeField] SelectEvent OnLose;
    [SerializeField] Gamble gamble;
    [SerializeField] TMP_Text moneyText;
    [SerializeField] Button reset;
    [SerializeField] AudioManager audioManager;

    public List<Item> items = new List<Item>();
    public int Money
    {
        get => money;
        set
        {
            moneyText.text = value.ToString();
            money = value;
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
        moneyText.text = "$" + Money.ToString();

    }
    public void GetMoney(ICreatureController creatureController)
    {
        if (creatureController.IsSelect)
        {
            Money += gamble.money * 2;
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
        if (Money >= GambleMoney)
        {

        }
        else
        {
            Money = 20;
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
