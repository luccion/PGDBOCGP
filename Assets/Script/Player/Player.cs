using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int money = 20;
    [SerializeField] int GambleMoney = 10;
    [SerializeField] SelectEvent selectEvent;
    [SerializeField] SelectEvent OnWin;
    [SerializeField] SelectEvent OnLose;
    [SerializeField] Gamble gamble;
    [SerializeField] TMP_Text moneyText;
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
    }

    private void OnDisable()
    {
        selectEvent.Unregister(SetNewGamble);
        OnWin.Unregister(GetMoney);
    }

    public void GetMoney(ICreatureController creatureController)
    {
        if (creatureController.IsSelect)
        {
            Money += gamble.money * 2;
        }
    }
    public void SetNewGamble(ICreatureController creatureController)
    {
        gamble = new();
        creatureController.IsSelect = true;
        gamble.money = GambleMoney;
        Money -= GambleMoney;
    }
}
class Gamble
{
    public ICreatureController creatureController;
    public int money;
}
