using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EndLine : Interaction
{
    [SerializeField] SelectEvent OnWin;
    [SerializeField] SelectEvent OnLose;
    public bool end = false;
    override public bool OnAfterInteract(ICreatureController creatureController)
    {
        if (!end)
        {
            end = true;

            if (creatureController.IsSelect)
            {
                OnWin.Invoke(creatureController);
                Debug.Log("win!!");
                return true;
            }
        }
        else
        {
            if (creatureController.IsSelect)
            {
                OnLose.Invoke(creatureController);
                Debug.Log("lose!!");
            }
        }
        return false;






    }
}
