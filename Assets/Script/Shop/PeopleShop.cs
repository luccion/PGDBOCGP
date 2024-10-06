using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleShop : MonoBehaviour
{
    [SerializeField] List<Transform> points;
    [SerializeField] List<Item> sellItems;
    [SerializeField] Collectable collectablePrefab;
    [SerializeField] ItemUI itemUI;
    Player player;
    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        RefleshShop();
    }
    public void RefleshShop()
    {
        foreach (var p in points)
        {
            Collectable collectable = Instantiate(collectablePrefab, p);
            collectable.Load(sellItems[0]);
            collectable.BuyEvent = OnBuy;
        }
    }
    void OnBuy(Item item)
    {
        if (player.Money >= item.price)
        {
            player.Money -= item.price;
            player.items.Add(item);
            itemUI.RefleshUI(player.items);
        }
    }
}
