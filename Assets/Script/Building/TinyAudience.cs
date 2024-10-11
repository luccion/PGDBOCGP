using UnityEngine;

public class TinyAudience : MonoBehaviour
{
    [SerializeField] SpriteRenderer audi;
    [SerializeField] Animator animator;

    void Start()
    {
        audi.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        float randomStart = Random.Range(0f, 1f);
        animator.Play("TinyAudience", 0, randomStart);
    }
}
