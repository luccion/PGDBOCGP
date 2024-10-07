using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;
public class TitleController : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] Button gogoBtn;
    [SerializeField] Button creditsBtn;
    [SerializeField] Button backToTitleBtn;
    // Start is called before the first frame update
    void Start()
    {


    }
    public void GoToTitle()
    {
        animator.SetTrigger("title");
    }
    public void GoToCredits()
    {
        Debug.Log("ddddd");
        animator.SetTrigger("credits");
    }
    public void GoToGameStart()
    {
        //咋写啊

    }

}
