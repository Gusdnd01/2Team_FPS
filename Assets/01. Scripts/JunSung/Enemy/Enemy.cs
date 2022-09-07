using System.Net.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public enum enemyState
    {
        Idle,
        Finding,
        Move,
        Attack,
        Die
    }
    public enemyState state = enemyState.Idle;

    [SerializeField] protected EnemyData data;
    [SerializeField] protected bool isAttack = false;
    [SerializeField] protected int hp;

    protected Transform playerTrm;
    protected NavMeshAgent nav;

    private void Awake() 
    {
        playerTrm = GameObject.Find("Player").GetComponent<Transform>();
        nav = GetComponent<NavMeshAgent>();

        hp = data.hp;
    }

    protected IEnumerator FSMCycle()
    {
        while (state != enemyState.Die)
        {
            switch(state)
            {
                case enemyState.Idle:
                    Idle();
                    break;
                case enemyState.Finding:
                    Finding();
                    break;
                case enemyState.Move:
                    Move();
                    break;
                case enemyState.Attack:
                    Attack();
                    break;
            }
        }
        OnDie();

        yield return new WaitForSeconds(0.1f);
    }

    protected abstract void Idle();
    protected abstract void Finding();
    protected abstract void Move();
    protected abstract void Attack();
    protected abstract void OnDie();
    public abstract void OnDamaged(int damage);

    protected float GetDistance()
    {
        return (playerTrm.position - transform.position).magnitude;
    }
}