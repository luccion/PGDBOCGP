using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Cinemachine;
using Unity.VisualScripting;

public class ClickHandler : MonoBehaviour
{
    public Camera mainCamera;
    PlayerInputActions playerInputs;
    public VoidEvent ReadyEvent;
    public SelectEvent StartRunEvent;
    // [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] PointerUI pointer;
    [SerializeField] GameCamera gameCamera;
    [SerializeField] GameManager gameManager;

    private void OnEnable()
    {
        playerInputs.Player.Click.performed += OnClickPerformed;
        StartRunEvent.Register(HandleClickOnCreature);
    }

    private void OnDisable()
    {
        playerInputs.Player.Click.performed -= OnClickPerformed;
        StartRunEvent.Unregister(HandleClickOnCreature);
    }
    private void Awake()
    {
        playerInputs = new PlayerInputActions();
        gameManager = FindAnyObjectByType<GameManager>();
        // 获取输入动作

    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0) && !gameManager.IsSelect)
            OnClickPerformed();
    }
    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        // 检查是否点击在 UI 上
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // 获取点击的世界坐标
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;  // 将 z 坐标设置为 0，因为在 2D 中 z 坐标不重要

        // 检查射线是否击中了某个 2D 物体
        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

        if (hit.collider != null)
        {
            // 获得被点击的 GameObject
            GameObject clickedObject = hit.collider.gameObject;

            // 检查对象是否是 Creature 类型
            CreatureController creature = clickedObject.GetComponent<CreatureController>();
            if (creature != null)
            {
                Debug.Log("Clicked on a Creature: " + creature.name);
                HandleClickOnCreature(creature);
            }
            else
            {
                Debug.Log("Clicked on a non-creature object: " + clickedObject.name);
            }
        }
    }
    void OnClickPerformed()
    {
        Debug.Log("cc");
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            GameObject clickedObject = hit.collider.gameObject;
            if (clickedObject.TryGetComponent<ICreatureController>(out ICreatureController creature))
            {
                //处理camera
                HandleClickOnCreature(creature);
                //触发其他事件
                StartRunEvent?.Invoke(creature);
                Debug.Log("Clicked on: " + clickedObject.name);
            }
            else
            {
                Debug.Log("Clicked on a non-creature object: " + clickedObject.name);

            }
        }

    }
    // 处理 Creature 类型物体的点击逻辑
    private void HandleClickOnCreature(ICreatureController creature)
    {
        gameCamera.Follow(creature.CreatureTransform);
        PointerUI obj = Instantiate(pointer, creature.CreatureTransform);
        obj.transform.localPosition = new Vector3(0, 4, 0);
    }
}
