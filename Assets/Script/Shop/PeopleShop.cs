using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleShop : MonoBehaviour
{
    [SerializeField] List<Item> sellItems;
    [SerializeField] Collectable collectablePrefab;
    [SerializeField] ItemUI itemUI;
    [SerializeField] Transform ShopPos;
    [SerializeField] AudioManager audioManager;
    [SerializeField] int nodeCount = 10;  // 房间节点数量
    [SerializeField] int nodesize = 5;  // 房间节点大小
    Player player;
    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        RefleshShop();
        for (int i = 0; i < 3; i++)
        {

        }
        itemUI.RefleshUI(player.items);
    }
    public void RefleshShop()
    {
        itemUI.RefleshUI(player.items);
        // 清除ShopPos下的所有子物体
        foreach (Transform child in ShopPos)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < nodeCount; i++)
        {
            int random = Random.Range(0, sellItems.Count);
            Collectable collectable = Instantiate(collectablePrefab, ShopPos);
            collectable.Load(sellItems[random]);
            collectable.transform.localPosition = new Vector2(nodesize * i, 0);
        }
        // collectable.BuyEvent = OnBuy;        }
    }
    public void OnBuy(Item item)
    {
        audioManager.PlayMoney();
        if (player.Money >= item.price)
        {
            player.Money -= item.price;
            player.items += 1;
            itemUI.RefleshUI(player.items);
        }
    }
}
