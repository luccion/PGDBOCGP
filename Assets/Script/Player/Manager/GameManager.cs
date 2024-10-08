using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject shutAnim;
    [SerializeField] List<CreatureController> tinyCreaturesPrefabs;
    [SerializeField] List<Transform> BirthPoint;
    [SerializeField] List<CreatureController> tinyCreatures;
    [SerializeField] Transform pos;
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
    [Header("chaser")]
    [SerializeField] CreatureController handObj;
    public AudioManager audioManager;
    Hand hand;
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
        OnWinEvent.Unregister(OnWin);
        OnLoseEvent.Unregister(OnLose);
    }
    private void Awake()
    {
        hand = handObj.GetComponent<Hand>();
        audioManager = gameObject.GetComponent<AudioManager>();
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
    private void FixedUpdate()
    {
        //Debug.Log("最后一名" + GetRank(5).Name);
    }
    public void Back2Menu()
    {
        SceneManager.LoadScene(0);
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

            CreatureController tinyPlayer = Instantiate<CreatureController>(tinyCreaturesPrefabs[numbers[i]], pos);
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
        handObj.StateMachine.TransitionTo(CreatureState.RUN);
    }
    void PlayerReady()
    {
        creatureControllers = GenerateCreature();
    }
    void OnWin(ICreatureController creatureController)
    {
        Debug.Log(creatureController.Name + "win");
        audioManager.PlayWin();
        //hand.Reset();
        StartCoroutine(StopAll(1, true));
    }
    void OnLose(ICreatureController creatureController)
    {
        audioManager.PlayDead();
        // hand.Reset();
        Debug.Log(creatureController.Name + "lose");
        StartCoroutine(StopAll(1, false));
    }
    public void ResetGame()
    {
        //  StopAllCoroutines();   

        foreach (var item in tinyCreatures)
        {
            Destroy(item.gameObject);
        }
        FindFirstObjectByType<EndLine>().end = false;
        FindFirstObjectByType<PeopleShop>().RefleshShop();
        Player player = FindFirstObjectByType<Player>();
        if (player.Money == 0)
        {
            // player.Money = 20;
            SceneManager.LoadScene(0);
        }
        handObj.StateMachine.TransitionTo(CreatureState.IDLE);
        handObj.SetSpeed(0);
        hand.Reset();
        cinemachineVirtualCamera.Follow = initFollow;
        IsSelect = false;
        OnReady.Invoke();
    }
    IEnumerator StopAll(int second, bool isWin)
    {

        yield return new WaitForSeconds(second);
        cinemachineVirtualCamera.Follow = handObj.transform;
        yield return KillAll(tinyCreatures, isWin);
        ResetGame();
    }
    IEnumerator KillAll(List<CreatureController> creatureControllers, bool isWin)
    {
        foreach (var item in creatureControllers)
        {
            item.StateMachine.TransitionTo(CreatureState.IDLE);
            item.SetSpeed(1);
        }
        foreach (var item in creatureControllers)
        {
            if (isWin && item.IsSelect)
            {
                continue;
            }
            yield return StartCoroutine(hand.FlytoKill(item));
        }
        handObj.StateMachine.TransitionTo(CreatureState.IDLE);
        yield return new WaitForSeconds(1f);
        handObj.SetSpeed(0);
        hand.Reset();
    }

    CreatureController GetRank(int rank)
    {
        List<CreatureController> rankedObjects = tinyCreatures
             .OrderByDescending(obj => obj.transform.position.x) // 根据 position.x 降序排序
             .ToList(); // 转换为列表
        // 打印排名
        return rankedObjects[rank - 1];
    }

}
