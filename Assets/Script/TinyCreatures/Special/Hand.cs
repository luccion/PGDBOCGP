using UnityEngine;

public class Hand : CreatureController
{
    public float requiredStayTime = 2f;  // 需要停留的时间（秒）
    public Vector2 boxSize = new Vector2(5, 5);  // 检测区域的大小
    public LayerMask targetLayer;  // 要检测的层级

    private float stayTimer = 0f;  // 计时器
    private bool creatureInRange = false;  // 标记对象是否在范围内

    private void Update()
    {
        // 如果 CreatureController 在范围内，则进行计时
        if (creatureInRange)
        {
            stayTimer += Time.deltaTime;

            if (stayTimer >= requiredStayTime)
            {
                // 停留时间超过设定值，执行目标函数
                ExecuteFunction();
                // 计时器重置
                stayTimer = 0f;
                creatureInRange = false;  // 重置范围标记
            }
        }
        else
        {
            // 如果对象不在范围内，重置计时器
            stayTimer = 0f;
        }
    }

    // 当物体进入触发器区域时
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsCreatureController(collision))
        {
            creatureInRange = true;  // 标记对象在范围内
        }
    }

    // 当物体在触发器区域内停留时
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!IsCreatureController(collision))
        {
            creatureInRange = false;
        }
    }

    // 当物体离开触发器区域时
    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (IsCreatureController(collision))
        {
            creatureInRange = false;  // 标记对象已离开范围
            stayTimer = 0f;  // 重置计时器
        }
    }

    // 判断是否为 CreatureController 对象
    private bool IsCreatureController(Collider2D collider)
    {
        return collider.GetComponent<CreatureController>() != null;
    }

    // 执行的目标函数
    private void ExecuteFunction()
    {
        Debug.Log("CreatureController 在范围内停留超过 " + requiredStayTime + " 秒，执行函数！");
        // 在这里添加你想执行的具体逻辑
    }

    // 绘制检测区域的 Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
