using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ��Ʃ����Ʈ public ����� ��� �ʵ带
[System.Serializable]
public class Playeranimation // �ν��Ͻ� â�� ���� �ش�.
{
    public AnimationClip idle;
    public AnimationClip runForward;
    public AnimationClip runBackward;
    public AnimationClip runLeft;
    public AnimationClip runRight;
    public AnimationClip Sprint;
}

public class Player : MonoBehaviour
{
    public Playeranimation Playeranimation;
    [SerializeField]
    private float movespeed = 5f;
    [SerializeField]
    private float rotspeed = 250f;
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    CapsuleCollider capsule;
    [SerializeField]
    Transform tr;
    [SerializeField]
    Animation _animation;
    float h, v, x;
    [SerializeField]
    private Transform firepos;
    
    private AudioSource source;
    public AudioClip clip;
    [SerializeField]
    private GameObject A4A1;
    [SerializeField]
    private GameObject ShotGun;
    [SerializeField]
    private bool DontFire=false;

    void Start()
    {
        // ���۳�Ʈ ĳ�� ó��
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();  
        _animation = GetComponent<Animation>();
        tr = GetComponent<Transform>();
        _animation.Play(Playeranimation.idle.name);
        firepos = GameObject.Find("fire").transform;

        clip = Resources.Load("Sound/p_m4_1") as AudioClip;   
        source = GetComponent<AudioSource>();
    }


    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        x = Input.GetAxisRaw("Mouse X");
        Vector3 moveDir = (h * Vector3.right) + (v * Vector3.forward);
        tr.Translate(moveDir.normalized * movespeed * Time.deltaTime, Space.Self);
        MoveAnimation();
        Runing();
        tr.Rotate(Vector3.up * x * Time.deltaTime * rotspeed);
        if (DontFire == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }
      
        

    }

    private void Fire()
    {
        var _bullet = ObjectPullingManger.pullingManger.GetBulletPool();
        if (_bullet != null)
        {
            _bullet.transform.position = firepos.position;
            _bullet.transform.rotation = firepos.rotation; 
            _bullet.SetActive(true);
        }
        source.PlayOneShot(clip, 1.0f);
        //var firebullet = Instantiate(bullet, firepos.position, firepos.rotation);
    }

    private void Runing()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) )
        {
            movespeed = 10f;
            _animation.CrossFade(Playeranimation.Sprint.name, 0.3f);
            DontFire = true;
        }
        else if (Input.GetKey(KeyCode.D)&& Input.GetKey(KeyCode.A)&& Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
        {
            movespeed = 10f;
           
            DontFire = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movespeed = 5f;
            DontFire = false;
        }
    }

    private void MoveAnimation()
    {
        if (h > 0.1f)
        {
            _animation.CrossFade(Playeranimation.runRight.name, 0.3f);
            // ���� ���� Ŭ���� �� ���� ����Ŭ�� �ִϸ��̼� 0.3�ʰ� ȥ���ϸ� �ε巯�� �ִϸ��̼��� ���´�.

        }
        else if (h < -0.1f)
        {
            _animation.CrossFade(Playeranimation.runLeft.name, 0.3f);
        }
        else if (v > 0.1)
        {
            _animation.CrossFade(Playeranimation.runForward.name, 0.3f);
        }
        else if (v < -0.1)
        {
            _animation.CrossFade(Playeranimation.runBackward.name, 0.3f);
        }
        else if (h == 0 && v == 0)
        {
            _animation.CrossFade(Playeranimation.idle.name);
        }
    }
}
