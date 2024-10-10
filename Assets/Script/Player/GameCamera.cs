using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Vector2 followPos;
    public Transform follower;
    public float smoothTime = 0.3f; // 平滑时间
    private Vector3 velocity = Vector3.zero; // 用于SmoothDamp的速度参数

    public void Follow(Transform transform)
    {
        follower = transform;
    }

    private void Update()
    {
        if (follower != null)
        {
            // 使用SmoothDamp来平滑移动
            Vector3 targetPosition = new Vector3(follower.position.x, 0, -10);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
