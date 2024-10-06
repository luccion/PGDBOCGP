using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TinyCreatureSO", menuName = "TinyCreatureSO", order = 0)]
public class TinyCreatureSO : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] TinyPersona tinyPersona;
    [Range(0, 10)]
    [SerializeField] int _acceleration = 1;
    [Range(0, 10)]
    [SerializeField] int _lucky = 5;
    [Range(0, 10)]
    [SerializeField] int _health = 5;
    [SerializeField] int _maxSpeed = 10;
    [SerializeField] CreatureController creatureController;
    //属性
    public int Acceleration => _acceleration;
    public int Lucky => _lucky;
    public int Health => _health;
    public TinyPersona TinyPersona => tinyPersona;
    public int MaxSpeed => _maxSpeed;
    public string Name => _name;
    public CreatureController CreatureController => creatureController;

}

public enum TinyPersona
{
    NONE,
    RUDE,
    FEARLESS,
}