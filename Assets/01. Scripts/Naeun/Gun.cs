using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public enum State
    {
        Ready, // �غ� �Ϸ�
        Shoot, // �߻� ��
        Reloading, // ������
        FineSight, // ������
    }
    public State state { get; private set; }

    [Header("data")]
    [SerializeField] protected WeaponDataSo weaponData;


    protected GameObject Player;

    protected float originTurnSpeed; //���� ��� ���� �÷��̾��� ���� ����

    [Header("sound & effect")]
    [SerializeField] protected Transform weaponMuzzle;
    [SerializeField] protected Transform powerShotPoint;
    [SerializeField] protected LineRenderer bulletLine;
    [SerializeField] protected GameObject aimPoint;

    protected RaycastHit hit; // �浹 ����

    [Header("bullet")]
    [SerializeField] protected int maxBullet; // �ִ� �Ѿ� ����
    [SerializeField] protected int mag; // �� źâ�� �� �� �ִ� �Ѿ� ����
    [SerializeField] protected int curBullet; // �� źâ�� ���� ���� �ִ� �Ѿ� ����
    [SerializeField] protected float reloadDelay;

    [Header("camera")]
    [SerializeField] protected Camera cam;

    [Header("")]
    [SerializeField] protected float curAttackDelay;
    [SerializeField] protected Vector3 originPos;
    [SerializeField] protected Vector3 fineSightPos; // ������ �� ��ġ

    protected void Awake()
    {
        state = State.Ready;
        bulletLine.enabled = false;
    }

    protected void Start()
    {
        if(state == State.Ready && Time.time >= weaponData.attackDelay)
        {

        }
    }

    protected void Update()
    {

    }

    void GunFireRate()
    {
        if (curAttackDelay > 0)
            curAttackDelay -= Time.deltaTime;
    }

    void TryFire()
    {
        GunFireRate();

        if(state == State.Ready)
        {
            if(curBullet > 0)
            {
                if(Input.GetMouseButtonDown(0) && curAttackDelay <= 0)
                {
                    Debug.Log(curAttackDelay);
                    state = State.Shoot;
                    LeftClick();
                }
                if (Input.GetMouseButtonUp(0))
                    state = State.Ready;
            }
            else
                StartCoroutine(Reloading());
        }
    }

    public override void LeftClick()
    {
        curAttackDelay = weaponData.attackDelay;
        curBullet--; 

        // �߻� ����Ʈ
    }
    protected virtual IEnumerator Reloading()
    {
        state = State.Reloading;
        yield return new WaitForSeconds(reloadDelay);
        maxBullet -= mag - curBullet;
        curBullet = mag;
        state = State.Ready;
    }

    public override void RightClick()
    {
        Vector3 lazerStartPos = weaponMuzzle.position;
        bulletLine.startWidth = 0.1f;
        bulletLine.endWidth = 0.2f;
        bulletLine.SetPosition(0, weaponMuzzle.position);
        bulletLine.SetPosition(1, aimPoint.transform.position);
        bulletLine.enabled = true;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weaponData.range))
        {
            if(hit.transform.tag == "Enemy")
            {
                //IDamageAble
            }
        }
    }

    public override void PressR()
    {
        if (Input.GetKeyDown(KeyCode.R) && state == State.Ready && curBullet < mag)
        {
            StartCoroutine(Reloading());
        }
    }
}
