using System.Net.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    protected enum enemyState
    {
        Idle, 
        Find,
        Move,
        Attack
    }
    [SerializeField] protected enemyState state = enemyState.Idle;

    [SerializeField] protected EnemyData data;

    [SerializeField] private bool isAttack = false;
    [SerializeField] protected int hp;
    private float currentFindTime = 0;

    protected Rigidbody rb;
    protected NavMeshAgent nav;
    protected Transform playerTrm;
    protected Transform attackPos;

    protected void SetValue()//Awake에 넣어줌
    {
        playerTrm = GameObject.Find("Player").GetComponent<Transform>();
        attackPos = transform.Find("AttackPoint").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();

        hp = data.hp;
    }

    protected void FSMCycle()//UpDate에 넣어줌
    {
        switch (state)
        {
            case enemyState.Idle:
                Idle();
                break;
            case enemyState.Find:
                Finding();
                break;
            case enemyState.Move:
                Move();
                break;
            case enemyState.Attack:
                Attack();
                break;
        } 

        if(hp <= 0)
        {
            Die();
        }
    }

    protected virtual void Idle()
    {
        Debug.Log("Idle");
        if((playerTrm.position - transform.position).magnitude <= data.findDistance)
        {
            state = enemyState.Find;
        }
    }

    protected virtual void Finding()
    {
        Debug.Log("Finding");
        currentFindTime += Time.deltaTime;

        if((playerTrm.position - transform.position).magnitude > data.findDistance)
        {
            state = enemyState.Idle;
            currentFindTime = 0;
        }
        else if(currentFindTime >= data.findTime)
        {
            state = enemyState.Move;
            currentFindTime = 0;
        }
    }

    protected virtual void Move()
    {
        Debug.Log("Move");

        nav.SetDestination(playerTrm.position);

        if((playerTrm.position - transform.position).magnitude <= data.attackDistance)
        {
            nav.SetDestination(transform.position);
            state = enemyState.Attack;
        }
        else if((playerTrm.position - transform.position).magnitude > data.findDistance)
        {
            nav.SetDestination(transform.position);
            state = enemyState.Idle;
        }
    }

    protected virtual void Attack()
    {
        if(!isAttack)
        {
            StartCoroutine(AttackCoroutine());
            Debug.Log("Attack");
        }
        
        transform.LookAt(playerTrm);
    }

    protected IEnumerator AttackCoroutine()
    {
        isAttack = true;

        yield return new WaitForSeconds(data.attackDelay);

        Collider[] cols = Physics.OverlapSphere(attackPos.position, data.attackRadius, 1 << 6);

        foreach(Collider player in cols)
        {
            Debug.Log("플레이어 맞아당 허허"); //player hit 넣으셈
        }

        if((playerTrm.position - transform.position).magnitude > data.attackDistance)
        {
            state = enemyState.Move;
        }

        isAttack = false;
    }

    protected virtual void Die()
    {
        //"해줘"
    }
    
    public void OnDamaged(int damage)
    {
        hp -= damage;
    }
}