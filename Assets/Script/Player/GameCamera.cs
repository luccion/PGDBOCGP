using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Vector2 followePos;
    public Transform follower;

    public void Follow(Transform transform)
    {
        follower = transform;
    }

    private void Update()
    {
        if (follower != null)
        {
            transform.position = new Vector3(follower.position.x, 0, -10);
        }
    }
}
