using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("source")]
    public AudioSource sfx;

    public AudioClip dual;
    public AudioClip dead;
    public AudioClip win;
    public AudioClip money;
    public AudioClip getMoney;
    public AudioClip useBanana;

    [Header("environment")]
    public AudioClip crowd;

    [Header("parse")]
    public AudioClip ready;
    public AudioClip run;

    [Header("character")]
    public AudioClip onBanana;
    public AudioClip jumpRock;

    // 通用的播放函数
    public void PlayOneshot(AudioClip audioClip)
    {
        sfx.PlayOneShot(audioClip);
    }

    // 用于跳跃的音效
    public void PlayJump()
    {
        PlayOneshot(jumpRock);
    }

    // 用于得到钱币的音效
    public void PlayGetMoney()
    {
        PlayOneshot(getMoney);
    }
    // 用于踩香蕉的音效
    public void PlayOnBanana()
    {
        PlayOneshot(onBanana);
    }

    // 用于战斗的音效
    public void PlayDual()
    {
        PlayOneshot(dual);
    }

    // 用于角色死亡的音效
    public void PlayDead()
    {
        PlayOneshot(dead);
    }

    // 用于胜利的音效
    public void PlayWin()
    {
        PlayOneshot(win);
    }

    // 用于获取金钱的音效
    public void PlayMoney()
    {
        PlayOneshot(money);
    }

    // 用于使用香蕉的音效
    public void PlayUseBanana()
    {
        PlayOneshot(useBanana);
    }

    // 用于环境音效 - 人群
    public void PlayCrowd()
    {
        PlayOneshot(crowd);
    }

    // 用于开始比赛时的音效
    public void PlayReady()
    {
        PlayOneshot(ready);
    }

    // 用于角色奔跑的音效
    public void PlayRun()
    {
        PlayOneshot(run);
    }
}
