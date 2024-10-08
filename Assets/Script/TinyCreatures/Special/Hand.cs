using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] AudioClip grasp;
    [SerializeField] Vector3 offset;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    Vector2 startPos;
    public IEnumerator FlytoKill(ICreatureController creatureController)
    {
        spriteRenderer.color = creatureController.tinyCreatureSO.BloodColor;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = creatureController.CreatureTransform.position + offset;
        float duration = 0.25f; // 移动持续时间
        float elapsedTime = 0f;

        // 执行线性移动
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        // 确保最终位置准确
        transform.position = targetPosition;

        // 等待动画时间（如果动画持续时间为 0.25f）
        animator.SetTrigger("Grasp");
        yield return new WaitForSeconds(0.25f);

        // 禁用对象
        creatureController.CreatureTransform.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.41f);
    }

    public void Reset()
    {
        transform.position = startPos;
    }
    private void Start()
    {
        startPos = transform.position;
    }
    public void PlayGrasp()
    {
        GetComponent<AudioSource>().PlayOneShot(grasp);
    }


}