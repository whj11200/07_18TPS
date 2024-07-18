using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class EnemyAi : MonoBehaviour
{
    public enum State// ������ ���
    {
        PTROL = 0, TRACE, ATTACK, DIE
    }
    public State state = State.PTROL;
    [SerializeField] private Transform Playertr; // �Ÿ��� ������� ����
    [SerializeField] private Transform Enemytr;  // �Ÿ��� ������� ����
    [SerializeField] private Animator animator; // �ִϸ�����
    // ���� �Ÿ� ���� �Ÿ�
    public float attackDist = 5.0f; // ���ݻ�Ÿ�
    public float traceDist = 10f;  // ���� ��Ÿ�
    public bool isDie = false; // �������
    private WaitForSeconds ws; // 
    private Enemy enemy;
    // �ִϸ����� ��Ʈ�ѷ��� ���� �� �Ķ������ �ؽð��� ������ �̸� ����
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
    private void OnEnable() //  ������Ʈ�� Ȱ��ȭ �ɶ� ȣ��
    {
        StartCoroutine(CacheState());
        StartCoroutine(Action());
    }
    IEnumerator CacheState()
    {
        while (!isDie)
        {
            if (state == State.DIE) yield break;
            // ��� �����̸� �ڷ�ƾ �Լ��� ���� ��Ŵ
            float dist = (Playertr.position - Enemytr.position).magnitude;
            // ���� ���ݰŸ��� ���´ٸ�
            if (dist <= attackDist)
            {
                // ATTACKȰ��ȭ
                state = State.ATTACK;
            }
            // �߰ݰŸ��� ������
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
