using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a : Enemy
{
    [SerializeField] private float detectAngle;
    [SerializeField] private bool isFind = false;

    private float currentAngle;

    private void Start()
    {
        StartCoroutine(FSMCycle());
    }

    protected override void Idle()
    {
        Debug.Log("idle");
        if(CheckAngle() || GetDistance() <= data.findDistance)
        {
            Debug.Log("find");
            state = enemyState.Move;
        }
    }

    protected override void Finding()
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {   
        Debug.Log("move");
        nav.SetDestination(playerTrm.position);

        if(data.attackDistance > GetDistance())
        {   
            nav.SetDestination(transform.position);
            Debug.Log("attack");
            state = enemyState.Attack;
        }
    }
    protected override void Attack()
    {
        Debug.Log("attack");
        if(!isAttack)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    protected override void OnDie()
    {
        throw new NotImplementedException();
    }

    public override void OnDamaged(int damage)
    {
        throw new NotImplementedException();
    }

    private IEnumerator AttackCoroutine()
    {
        isAttack = true;

        yield return new WaitForSeconds(data.attackDelay);

        Collider[] cols = Physics.OverlapSphere(transform.position + transform.forward, data.attackRadius, 1 << 6);
        
        if(cols != null)
        {
            foreach(Collider col in cols)
            {
                col.GetComponent<IDamageable>().OnDamaged(data.attackPower);
            }
        }

        if(data.attackDistance < GetDistance())
        {
            state = enemyState.Move;
        }

        isAttack = false;
    }

    private float GetAngle()
    {
        Vector3 dir = (playerTrm.position - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, dir);

        return Mathf.Acos(dot) * Mathf.Rad2Deg;
    }

    private bool CheckAngle()
    {
        if(detectAngle >= GetAngle()) { return true; }
        else { return false; }
    }
}
