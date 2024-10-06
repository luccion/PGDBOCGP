using System;
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

    [Header("显示")]
    [SerializeField] EyeType eyeType = EyeType.BigEyes;
    public enum EyeType
    {
        BigEyes,
        BigBlueEyes,
        WiseEyes,
        NoEyes,
    }

    [Header("预设")]
    [SerializeField] string _name;
    [SerializeField] float jumpForce;
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] int acceleration;
    [SerializeField] int lucky;

    [Header("互动")]
    [SerializeField] float pickupRadius = 5f;
    public PlayerInputActions inputControl;
    [Header("状态机")]
    private StateMachine playerStateMachine;
    [Header("当前状态")]
    bool isStop;
    public Transform PlayerTransform => transform;
    public string Name => _name;
    public Transform CreatureTransform => transform;

    public StateMachine StateMachine => playerStateMachine;

    public void TransitionTo(CreatureState gameState) => playerStateMachine.TransitionTo(gameState);
    [SerializeField] Vector2 moveDelta;


    private void Awake()
    {
        inputControl = new PlayerInputActions();
        inputControl.Enable();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //初始化值
        speed = tinyCreature.Acceleration;
        maxSpeed = tinyCreature.MaxSpeed;
        acceleration = tinyCreature.Acceleration;
        _name = tinyCreature.Name;
        lucky = tinyCreature.Lucky;
        //初始化状态机
        playerStateMachine = new StateMachine(this);
        playerStateMachine.Initialize(CreatureState.IDLE);

        //初始化眼睛状态机
        switch (eyeType)
        {
            case EyeType.BigEyes:
                break;
            case EyeType.BigBlueEyes:
                break;
            case EyeType.WiseEyes:
                break;
            case EyeType.NoEyes:
                break;
            default:
                break;
        }
        //设置动画状态机
    }


    private void Update()
    {
        playerStateMachine.Update();
    }
    public void LoadData(TinyCreatureSO tinyCreature)
    {
        speed = tinyCreature.Acceleration;
        maxSpeed = tinyCreature.MaxSpeed;
        acceleration = tinyCreature.Acceleration;
        _name = tinyCreature.Name;
        lucky = tinyCreature.Lucky;
    }
    public bool GetIsStop()
    {
        return false;
    }
    public void HandleMove()
    {
        // 当前速度的大小（忽略方向）
        float currentSpeed = rb.velocity.magnitude;

        // 当速度小于最大速度时继续加速
        if (currentSpeed < maxSpeed)
        {
            // 计算加速方向
            Vector3 direction = transform.right;

            // 添加加速度
            rb.AddForce(direction * acceleration);
            speed = rb.velocity.magnitude;
            // 限制速度不超过最大值
            if (speed > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed; // 设置速度为最大值，保持方向
            }
        }
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
    public void SetSpeed(int speed)
    {
        // 将物体的线性速度和角速度设为零
        rb.velocity = speed * transform.right;
        rb.angularVelocity = speed;

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
    IEnumerator Stoper(int speed, float second)
    {
        SetSpeed(speed);
        yield return new WaitForSeconds(speed);
        TransitionTo(CreatureState.RUN);
    }
    public void SetStop()
    {
        StartCoroutine(Stoper(0, 2f));
    }
    public bool GetLucky()
    {
        int random = UnityEngine.Random.Range(0, 10);
        if (lucky > random)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("encounter");
        if (other.TryGetComponent<Interaction>(out Interaction i))
        {
            //自动互动
            i.OnInteract(this);
        }
    }
}

