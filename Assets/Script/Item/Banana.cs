using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 一次性香蕉
/// </summary> <summary>
/// 
/// </summary>
public class Banana : Item
{

    public override bool OnInteract(ICreatureController creatureController)
    {
        if (!isUsed)
        {
            creatureController.RunCoroutine(BlockCoroutine(creatureController));

            return true;
        }
        else
        {
            isUsed = true;
            return false;
        }


    }
    IEnumerator BlockCoroutine(ICreatureController creatureController)
    {
        // 获取当前的 Z 轴旋转角度
        float startRotation = creatureController.CreatureTransform.eulerAngles.z;
        float endRotation = startRotation + 360; // 目标 Z 轴旋转 360 度
        float rotationDuration = 0.5f; // 旋转持续时间
        float elapsedTime = 0f;


        creatureController.StateMachine.TransitionTo(CreatureState.BLOCK);
        creatureController.SetSpeed(0);

        // 在 rotationDuration 时间内逐步旋转
        while (elapsedTime < rotationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / rotationDuration;

            // 逐帧更新 Z 轴的旋转角度
            float currentZRotation = Mathf.Lerp(startRotation, endRotation, t);
            creatureController.CreatureTransform.eulerAngles = new Vector3(0, 0, currentZRotation);

            yield return null; // 等待下一帧
        }
        yield return new WaitForSeconds(rotationDuration);
        Destroy(gameObject);
        creatureController.StateMachine.TransitionTo(CreatureState.RUN);



        Debug.Log("oops");
    }


}
