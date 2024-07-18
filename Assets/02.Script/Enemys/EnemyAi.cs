using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class EnemyAi : MonoBehaviour
{
    public enum State// 열거형 상수
    {
        PTROL = 0, TRACE, ATTACK, DIE
    }
    public State state = State.PTROL;
    [SerializeField] private Transform Playertr; // 거리를 재기위해 선언
    [SerializeField] private Transform Enemytr;  // 거리를 재기위해 선언
    [SerializeField] private Animator animator; // 애니메이터
    // 공격 거리 추적 거리
    public float attackDist = 5.0f; // 공격사거리
    public float traceDist = 10f;  // 추적 사거리
    public bool isDie = false; // 사망여부
    private WaitForSeconds ws; // 
    private Enemy enemy;
    // 애니메이터 컨트롤러에 정의 한 파라미터의 해시값을 정수로 미리 추출
    private readonly int hashMove = Animator.StringToHash("Is_Move");
    private readonly int hashSpeed = Animator.StringToHash("MoveSpeed");
    private EnemyFire enemyFire;
    void Awake()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        enemyFire = GetComponent<EnemyFire>();
        var Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null)
        {
            Playertr = Player.GetComponent<Transform>();
        }
        Enemytr = GetComponent<Transform>();
        ws = new WaitForSeconds(0.3f);
    }
    private void OnEnable() //  오브젝트가 활성화 될때 호출
    {
        StartCoroutine(CacheState());
        StartCoroutine(Action());
    }
    IEnumerator CacheState()
    {
        while (!isDie)
        {
            if (state == State.DIE) yield break;
            // 사망 상태이면 코루틴 함수를 종료 시킴
            float dist = (Playertr.position - Enemytr.position).magnitude;
            // 만약 공격거리에 들어온다면
            if (dist <= attackDist)
            {
                // ATTACK활성화
                state = State.ATTACK;
            }
            // 추격거리에 들어오면
            else if (dist <= traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.PTROL;
            }
            yield return ws;
        }

    }
    IEnumerator Action()
    {
        while (!isDie)
        {
            yield return ws;
            switch (state)
            {
                case State.PTROL:
                    enemyFire.isFire = false;
                    enemy.patroiing = true;
                    animator.SetBool(hashMove, true);
                    break;
                case State.ATTACK:
                    enemyFire.isFire = true;
                    enemy.Stop();
                    animator.SetBool(hashMove, false);
                    break;
                case State.TRACE:
                    enemyFire.isFire = false;
                    enemy.traceTaget = Playertr.position;
                    animator.SetBool(hashMove, true);
                    break;
                case State.DIE:
                    enemy.Stop();
                    enemyFire.isFire = false;
                    break;
            }
        }
        
    }


    void Update()
    {
        animator.SetFloat(hashSpeed,enemy.speed);
    }
}
