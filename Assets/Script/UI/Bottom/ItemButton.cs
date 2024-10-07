using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 其中的物品放置到场景中
/// </summary> <summary>
/// 
/// </summary>
public class ItemButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Item theItem;
    [SerializeField] Image image;
    bool isDragging = false;
    GameObject current;
    [SerializeField] GameManager gameManager;
    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    public void Load(Item theItem)
    {
        image.sprite = theItem.spriteRenderer.sprite;
        this.theItem = theItem;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

        if (theItem == null) return;
        image.color = Color.clear;
        current = null;
        isDragging = true;
        // 获取鼠标在屏幕上的位置
        Vector3 screenPosition = eventData.position;

        // 将屏幕坐标转换为世界坐标
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane));

        // 根据需求调整 z 轴值，如果是 2D 游戏，可以将 z 设为 0
        worldPosition.z = 0;

        // 在指定位置生成物体
        Item obj = Instantiate(theItem, worldPosition, Quaternion.identity);

        current = obj.gameObject;
        current.GetComponent<BoxCollider2D>().enabled = false;
        Debug.Log("" + obj.name);

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 screenPosition = eventData.position;

        // 将屏幕坐标转换为世界坐标
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, Camera.main.nearClipPlane));

        // 根据需求调整 z 轴值，如果是 2D 游戏，可以将 z 设为 0
        worldPosition.z = 0;
        current.gameObject.transform.position = worldPosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {

        gameManager.audioManager.PlayOneshot(theItem.audioClip);
        isDragging = false;
        Destroy(gameObject);
        current.GetComponent<BoxCollider2D>().enabled = true;
        FindFirstObjectByType<Player>().items.Remove(theItem);
    }
}
