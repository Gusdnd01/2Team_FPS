using System.Net.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    private enum enemyState
    {
        Idle, 
        Find,
        Move,
        Attack
    }
    [SerializeField] private enemyState state = enemyState.Idle;

    [SerializeField] private EnemyData data;

    [SerializeField] private bool isAttack = false;
    [SerializeField] private int hp;
    private float currentFindTime = 0;

    private Rigidbody rb;
    private NavMeshAgent nav;
    private Transform playerTrm;
    private Transform attackPos;

    private void Awake() 
    {
        playerTrm = GameObject.Find("Player").GetComponent<Transform>();
        attackPos = transform.Find("AttackPoint").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();

        hp = data.hp;
    }

    private void Update() 
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

    private void Idle()
    {
        Debug.Log("Idle");
        if((playerTrm.position - transform.position).magnitude <= data.findDistance)
        {
            state = enemyState.Find;
        }
    }

    private void Finding()
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

    private void Move()
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

    private void Attack()
    {
        if(!isAttack)
        {
            StartCoroutine(AttackCoroutine());
            Debug.Log("Attack");
        }
        
        transform.LookAt(playerTrm);
    }

    private IEnumerator AttackCoroutine()
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

    private void Die()
    {
        //"해줘"
    }
    
    public void Damaged(int damage)
    {
        hp -= damage;
    }
}