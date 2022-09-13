using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public enum State
    {
        Ready, // 준비 완료
        Shoot, // 발사 중
        Reloading, // 재장전
        FineSight, // 정조준
    }
    public State state { get; private set; }

    [Header("data")]
    [SerializeField] protected WeaponDataSo weaponData;


    protected GameObject Player;

    protected float originTurnSpeed; //감도 제어를 위한 플레이어의 원래 감도

    [Header("sound & effect")]
    [SerializeField] protected Transform weaponMuzzle;
    [SerializeField] protected Transform powerShotPoint;
    [SerializeField] protected LineRenderer bulletLine;
    [SerializeField] protected GameObject aimPoint;

    protected RaycastHit hit; // 충돌 정보

    [Header("bullet")]
    [SerializeField] protected int maxBullet; // 최대 총알 개수
    [SerializeField] protected int mag; // 한 탄창에 들어갈 수 있는 총알 개수
    [SerializeField] protected int curBullet; // 한 탄창에 현재 남아 있는 총알 개수
    [SerializeField] protected float reloadDelay;

    [Header("camera")]
    [SerializeField] protected Camera cam;

    [Header("")]
    [SerializeField] protected float curAttackDelay;
    [SerializeField] protected Vector3 originPos;
    [SerializeField] protected Vector3 fineSightPos; // 정조준 시 위치

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

        // 발사 이펙트
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
