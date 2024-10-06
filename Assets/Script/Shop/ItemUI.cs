using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    [SerializeField] ItemButton itemButtonPrefab;
    public void RefleshUI(List<Item> items)
    {
        DestroyChildren();

        foreach (var item in items)
        {
            ItemButton itemButton = Instantiate(itemButtonPrefab, transform);
            itemButton.Load(item);
        }
    }

    public void DestroyChildren()
    {
        // 获取父对象的所有子对象数量
        int childCount = transform.childCount;

        // 从最后一个子对象开始销毁，避免索引混乱
        for (int i = childCount - 1; i >= 0; i--)
        {
            // 获取子对象
            GameObject child = transform.GetChild(i).gameObject;

            // 销毁子对象
            Destroy(child);
        }
    }
}
