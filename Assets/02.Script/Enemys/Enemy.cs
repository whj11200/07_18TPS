using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    // ��Ʈ�� ������ ��� ����  List ���׸�(�Ϲ���) ����
    private NavMeshAgent agent;
    public List<Transform> WayPointList; //  ���� ������ �迭 �ε��� ��
    public int NextIdex = 0; //
    private bool _patrolling;
    private readonly float petrolSpeed = 1.5f;
    private readonly float turnSpeed = 1.5f;
    private float damping = 1.0f; //ȸ���Ҷ� �ӵ� ���� ���
    private Transform Enemytr;
    
    public bool patroiing // ���� ��Ƽ
    {
        get { return _patrolling; }
        set
        {
            _patrolling = value;
            if (_patrolling)
            {
                agent.speed = petrolSpeed;
                damping = 1.0f;
                MoveWayPoint();
            }


        }
    }
    // 
    private Vector3 _traceTaget;
    public Vector3 traceTaget
    {
        // ����� _traceTaget��  ������
        get { return _traceTaget; }
        set
        {
            // ���ǵ� ���� �Լ��� ���� �Ҵ�
            _traceTaget = value;
            agent.speed = petrolSpeed;
            damping = 7.0f;
            TraceTarget(_traceTaget);
        }
    }
    public float speed
    {
        // NevMesh
        get { return agent.velocity.magnitude;}
    }

    void Start()
    {
        Enemytr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        agent.updateRotation = false;
        // ������Ʈ �̿��Ͽ� ȸ���ϴ� ����� ��Ȱ��ȭ�ϴ� ����  ���� �ε巴���ʤ���
        var Group = GameObject.Find("WayPointGroup");
        // ���̶�Ű�̿� �ִ� ���� ������Ʈ�� �� WayPointGrounp �� ã�Ƽ� ����
        if (Group != null) // ��ȿ�� �˻�
        {
            // ���� ������Ʈ�� Ʈ���� ���� wayPoint List�� �� ��´�.
            Group.GetComponentsInChildren<Transform>(WayPointList);
            WayPointList.RemoveAt(0); // ù��° �ε����� ����
            
        }
        MoveWayPoint();
    }

   
    void Update()
    {
        // ��� �������̶��
        if (agent.isStopped == false)
        {
            // NavMeshAgent ���� �� ���� ���͸� ���ʹϾ� Ÿ���� ������ ��ȯ
            Quaternion rot = Quaternion.LookRotation(agent.desiredVelocity);
            //                  �����Լ��� �̿��ؼ� ���������� �ε巴�� ȸ�� ��ų�� �ִ�.
            Enemytr.rotation = Quaternion.Slerp(Enemytr.rotation,rot,Time.deltaTime*damping);
        }
        if (_patrolling == false) return;

        //float distance = Vector3.Distance(transform.position, WayPointList[NextIdex].position);
        //float distance2 =(WayPointList[NextIdex].position-transform.position).magnitude;
              // ���� ���������� 0.5f���� �۴ٸ�
        if (agent.remainingDistance <= 0.5f)
        {
            agent.speed = 6f;
            NextIdex = ++NextIdex % WayPointList.Count;
            MoveWayPoint();
        }
    }

    void MoveWayPoint()
    {
        // �ִ� ��� ����� ������ �ʰ� ���� �Ҿ�������
        if (agent.isPathStale)
        {
            return;
        }
        // �׺�޽� �����Ÿ��� waypointlist �迭�� ��ġ�� ���ƾ��Ѵ�.
        agent.destination = WayPointList[NextIdex].position;
        agent.isStopped = false;
    }

    private void TraceTarget(Vector3 pos)
    {
        if (agent.isPathStale) return;
        agent.destination = pos;
        agent.isStopped = false;
    }
    /// <summary>
    ///  ���ߴ� �Լ�
    /// </summary>
    public void Stop()
    {
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        _patrolling = false;
    }
}
