using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 其中的物品放置到场景中
/// </summary> <summary>
/// 
/// </summary>
public class ItemButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    GameObject theItem;

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("zzzz");
    }

    public void OnDragEnd(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 获取鼠标在屏幕上的位置
        Vector3 screenPosition = eventData.position;

        // 将屏幕坐标转换为世界坐标
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane));

        // 根据需求调整 z 轴值，如果是 2D 游戏，可以将 z 设为 0
        worldPosition.z = 0;

        // 在指定位置生成物体
        GameObject obj = Instantiate(theItem, worldPosition, Quaternion.identity);
        Debug.Log("" + obj.name);
    }
}
