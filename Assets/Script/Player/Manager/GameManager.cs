using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<CreatureController> tinyCreaturesPrefabs;
    [SerializeField] List<Transform> BirthPoint;
    [SerializeField] List<CreatureController> tinyCreatures;
    List<CreatureController> creatureControllers;
    //选人
    [SerializeField] UnityEvent OnPlayerReadyAction;
    //预备
    [SerializeField] UnityEvent OnReadyEvent;
    //跑
    [SerializeField] UnityEvent OnRunAction;
    [SerializeField] VoidEvent OnReady;
    [SerializeField] SelectEvent OnStartRun;
    [SerializeField] SelectEvent OnWinEvent;
    [SerializeField] SelectEvent OnLoseEvent;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] Transform initFollow;
    //本局游戏是否已经选择过
    public bool IsSelect = false;
    // Start is called before the first frame update
    private void OnEnable()
    {
        OnReady.Register(PlayerReady);
        OnStartRun.Register(RunGameCoroutine);
        OnWinEvent.Register(OnWin);
        OnLoseEvent.Register(OnLose);
    }
    private void OnDisable()
    {
        OnReady.Unregister(PlayerReady);
        OnStartRun.Unregister(RunGameCoroutine);
        OnWinEvent.Register(OnWin);
        OnLoseEvent.Register(OnLose);
    }
    void Start()
    {
        OnPlayerReadyAction?.Invoke();
        //触发准备事件
        OnReady.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public List<CreatureController> GenerateCreature()
    {
        List<CreatureController> creatureControllers = new();
        tinyCreatures = new();
        List<int> numbers = new List<int>();
        for (int i = 0; i < tinyCreaturesPrefabs.Count; i++)
        {
            numbers.Add(i);
        }

        // 打乱列表顺序（Fisher-Yates shuffle算法）
        for (int i = numbers.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = numbers[i];
            numbers[i] = numbers[randomIndex];
            numbers[randomIndex] = temp;
        }
        for (int i = 0; i < 5; i++)
        {

            CreatureController tinyPlayer = Instantiate<CreatureController>(tinyCreaturesPrefabs[numbers[i]]);
            TinyCreatureSO tinyCreatureSO = tinyPlayer.tinyCreatureSO;
            tinyPlayer.LoadData(tinyCreatureSO);
            tinyPlayer.transform.position = BirthPoint[i].position;

            creatureControllers.Add(tinyPlayer);
            tinyCreatures.Add(tinyPlayer);
        }
        return creatureControllers;
    }
    public void RunGameCoroutine(ICreatureController creatureController)
    {
        StartCoroutine(RunGame());
    }
    public IEnumerator RunGame()
    {
        OnReadyEvent?.Invoke();
        yield return new WaitForSeconds(3.5f);
        OnRunAction?.Invoke();
        foreach (var tiny in creatureControllers)
        {
            tiny.StateMachine.TransitionTo(CreatureState.RUN);
        }
    }
    void PlayerReady()
    {
        creatureControllers = GenerateCreature();
    }
    void OnWin(ICreatureController creatureController)
    {
        Debug.Log(creatureController.Name + "win");
        //StartCoroutine(StopAll());
    }
    void OnLose(ICreatureController creatureController)
    {
        Debug.Log(creatureController.Name + "lose");
        // StartCoroutine(StopAll());
    }
    public void ResetGame()
    {
        StopAllCoroutines();
        foreach (var item in tinyCreatures)
        {
            Destroy(item.gameObject);
        }
        FindFirstObjectByType<EndLine>().end = false;
        cinemachineVirtualCamera.Follow = initFollow;
        IsSelect = false;
        OnReady.Invoke();
    }
    IEnumerator StopAll()
    {
        yield return new WaitForSeconds(3);
        foreach (var item in tinyCreatures)
        {
            item.SetSpeed(0);
            item.TransitionTo(CreatureState.IDLE);
            item.Animator.Play("Idle");

        }
    }

}
