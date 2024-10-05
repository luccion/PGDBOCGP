using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class CreatureController : MonoBehaviour, ICreatureController
{
    [Header("数据")]
    [SerializeField] TinyCreatureSO tinyCreature;
    [Header("组件")] Rigidbody2D rb;
    Animator animator;
    [SerializeField] SpriteRenderer unit;

    [Header("预设")]
    [SerializeField] float jumpForce;
    [SerializeField] float speed;
    [Header("互动")]
    [SerializeField] float pickupRadius = 5f;
    public PlayerInputActions inputControl;
    [Header("状态机")]
    private StateMachine playerStateMachine;
    [Header("当前状态")]
    bool isStop;

    public Transform PlayerTransform => transform;
    public void TransitionTo(CreatureState gameState) => playerStateMachine.TransitionTo(gameState);
    [SerializeField] Vector2 moveDelta;
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    private void Awake()
    {
        inputControl = new PlayerInputActions();

        //定义一个背包
        inputControl.Enable();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = tinyCreature.Accelerate;
        playerStateMachine = new StateMachine(this);
        playerStateMachine.Initialize(CreatureState.RUN);
    }
    private void Start()
    {

    }
    private void Update()
    {
        playerStateMachine.Update();
    }
    public bool GetIsStop()
    {
        return false;
    }

    public void HandleMove()
    {
        rb.velocity = new Vector2(0, speed * Time.deltaTime);
        animator.SetFloat("moveX", moveDelta.x);
        animator.SetFloat("moveY", moveDelta.y);
        // if (moveDelta.x < 0)//left
        // {
        //     unit.flipX = true;
        // }
        // else
        // {
        //     unit.flipX = false;
        // }
    }
    /// <summary>
    /// 播放互动动画
    /// </summary>
    /// <returns></returns>
    public IEnumerator PlayInteractionAnim()
    {
        yield return null;
        // MapObject mapObj = currentObj;
        // if (mapObj == null)
        // {
        //     yield break;
        // }
        // inputControl.Disable();
        // switch (mapObj)
        // {
        //     case Tree:
        //         animator.Play("Chop");
        //         mapObj.GetComponent<Animator>().Play("Chop");
        //         //UIManager.Instance.FindView<BottomTools>().GetComponent<TimeControl>().CustomFastForward(20);
        //         yield return new WaitForSeconds(1.5f);
        //         //GameSetting.Instance.Continue();
        //         animator.Play("Idle");
        //         //MainMap.FreezeAllUnitsController(false);
        //         break;
        //     default:
        //         Debug.Log("缺少互动动画");
        //         break;
        // }
        // inputControl.Enable();
    }
}

