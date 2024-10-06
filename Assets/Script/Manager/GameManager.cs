using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<TinyCreatureSO> tinyCreatureSOs;
    [SerializeField] List<Transform> BirthPoint;
    List<CreatureController> creatureControllers;
    [SerializeField] UnityAction OnReady;
    [SerializeField] UnityAction OnStartRun;
    // Start is called before the first frame update
    void Start()
    {
        PlayerReady();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public List<CreatureController> GenerateCreature()
    {
        List<CreatureController> creatureControllers = new();
        List<int> numbers = new List<int>();
        for (int i = 0; i < BirthPoint.Count; i++)
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
        for (int i = 0; i < numbers.Count; i++)
        {

            TinyCreatureSO tinyCreatureSO = tinyCreatureSOs[i];
            CreatureController tinyPlayer = Instantiate<CreatureController>(tinyCreatureSO.CreatureController);
            tinyPlayer.LoadData(tinyCreatureSO);
            tinyPlayer.transform.position = BirthPoint[numbers[i]].position;
            creatureControllers.Add(tinyPlayer);
        }
        return creatureControllers;
    }
    public void RunGameCoroutine()
    {
        StartCoroutine(RunGame());
    }
    public IEnumerator RunGame()
    {
        yield return new WaitForSeconds(2f);
        OnStartRun?.Invoke();

        foreach (var tiny in creatureControllers)
        {
            tiny.StateMachine.TransitionTo(CreatureState.RUN);
        }
    }
    void PlayerReady()
    {
        creatureControllers = GenerateCreature();
        OnReady?.Invoke();
    }
}
