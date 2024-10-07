using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Collectable : MonoBehaviour
{
    Player player;
    public UnityAction<Item> BuyEvent;
    public Item item;
    [SerializeField] SpriteRenderer spriteRenderer;
    private void Start()
    {
        player = FindAnyObjectByType<Player>();
    }
    public void Load(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.spriteRenderer.sprite;
    }
    private void OnMouseDown()
    {
        Debug.Log("sss");
        Buy();
    }
    public void Buy()
    {

        if (player.Money >= item.price)
        {
            FindFirstObjectByType<PeopleShop>().OnBuy(item);
            //BuyEvent?.Invoke(item);
            Destroy(gameObject);
        }

    }
}
