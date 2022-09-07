using System.Net;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "SO/EnemyData", order = 0)]
public class EnemyData : ScriptableObject 
{
    public int hp;
    public int attackPower;
    public float moveSpeed;
    public float attackRadius;
    public float findDistance;
    public float findTime;
    public float moveDistance;
    public float attackDistance;
    public float attackDelay;
}
