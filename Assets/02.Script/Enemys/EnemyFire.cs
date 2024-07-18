using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    [SerializeField] private AudioClip firecilp;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playertr;
    [SerializeField] private Transform EnmeyTr;
    [SerializeField] private Transform FirePos;
    [SerializeField] private string Player = "Player";
    private readonly int hashFire = Animator.StringToHash("Fire");
    private float newxtFire = 0.0f; // ���� �ð��� �߻��� �ð� ���� ����
    private readonly float fireRate = 0.1f;// �Ѿ� �߻� ����
    private readonly float damping = 10.0f; // �÷��̾ ���� ȸ���� �ӵ�
    public bool isFire = false;
    

    void Start()
    {
        firecilp = Resources.Load ("Sound/p_shot_gun_1 1" )as AudioClip;
        animator = GetComponent<Animator> ();
        playertr = GameObject.FindWithTag(Player).transform;
        EnmeyTr = transform;
        FirePos = transform.GetChild(3).GetChild(0).GetChild(0).transform;
    }

   
    void Update()
    {
        if (isFire)
        {
            if (Time.time >= newxtFire)
            {
                Fire();
                
                newxtFire = Time.time + fireRate + Random.Range(0.0f, 0.3f);
            }
            Quaternion rot = Quaternion.LookRotation(playertr.position - EnmeyTr.position);
            EnmeyTr.rotation = Quaternion.Slerp(EnmeyTr.rotation, rot, damping * Time.deltaTime);
        }
    }
    
    void Fire()
    {
        animator.SetTrigger(hashFire);
        SoundManger.S_Instance.PlaySound(FirePos.transform.position, firecilp);

        var E_bullet = ObjectPullingManger.pullingManger.CreatEnBulletPool();
        if (E_bullet != null)
        {
            E_bullet.transform.position = FirePos.position;
            E_bullet.transform.rotation = FirePos.rotation;
            E_bullet.SetActive(true);
        }

    }

}
