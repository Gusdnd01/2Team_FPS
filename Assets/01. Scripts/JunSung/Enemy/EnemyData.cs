using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "SO/EnemyData", order = 0)]
public class EnemyData : ScriptableObject 
{
    public enum enemyState
    {
        Idle,
        Find,
        Move,
        Attack,
    }
    
    public enemyState state;
    public int hp;
    public int attackPower;
    public float moveSpeed;
    public float attackRange;
    public float moveDistance;
    public float attackDistance;
}
