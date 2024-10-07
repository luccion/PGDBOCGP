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
        transform.position = creatureController.CreatureTransform.position + offset;
        spriteRenderer.color = creatureController.tinyCreatureSO.BloodColor;
        animator.SetTrigger("Grasp");
        yield return new WaitForSeconds(0.25f);
        creatureController.CreatureTransform.gameObject.SetActive(false);
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