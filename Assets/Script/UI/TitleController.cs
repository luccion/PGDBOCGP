using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleController : MonoBehaviour
{

    [SerializeField] Animator animator;


    public void GoToTutorial()
    {
        animator.SetTrigger("tutorial");
    }

    public void GoToTitle()
    {
        animator.SetTrigger("title");
    }
    public void GoToCredits()
    {
        animator.SetTrigger("credits");
    }


}
