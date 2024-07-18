using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// �÷��̰� �Ǿ����� �跲 ������ �����ϰ� �����
// 5�� �跲�� �Ѿ˿� ���� �������� �����
public class BarrelCtrl : MonoBehaviour
{
    [SerializeField]
    private Texture[] textures;
    [SerializeField]
    private MeshRenderer mesh;
    [SerializeField]
    private int HitCount = 0;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private string BulletTag = "Bullet";
    private string E_BulletTag = "E_Bullet";
    [SerializeField]
    private GameObject ExplosionPrefad;
    private AudioClip clip;
    private AudioClip BoomCilp;

  
    void Start()
    {
        ExplosionPrefad = Resources.Load("EffectBoom") as GameObject;
        rb = GetComponent<Rigidbody>();
        textures = Resources.LoadAll<Texture>("BarrelTextures");
        // textrues = Resources.LoadAll("")as <Texture>;
        mesh = GetComponent<MeshRenderer>();
        mesh.material.mainTexture = textures[Random.Range(0, textures.Length)];
        clip = Resources.Load("Sound/bullet_hit_metal_enemy_1") as AudioClip;
        BoomCilp = Resources.Load("Sound/grenade_exp2") as AudioClip;

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(BulletTag)||other.gameObject.CompareTag(E_BulletTag))
        {
           other.gameObject.SetActive(false);
            SoundManger.S_Instance.PlaySound(transform.position, clip);
            if (++HitCount == 5)
            {
                ExplosionBarrel();
            }
            
            
        }
    }
    void ExplosionBarrel()
    {
        
        GameObject Effect = Instantiate(ExplosionPrefad,transform.position, Quaternion.identity);
        Destroy(Effect, 2f);
        Collider[] colls = Physics.OverlapSphere(transform.position, 20f);
        // �跲 �ڱ��ڽ� ��ġ���� 20���濡 �跲 ���̾ Cols ��� �迭�� ��´�.
      
        foreach (Collider coll in colls)
        {
            Rigidbody rigidbody = coll.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                SoundManger.S_Instance.PlaySound(transform.position, BoomCilp);
                rigidbody.mass = 1.0f;
                rigidbody.AddExplosionForce(500, transform.position, 10f, 1000f);
                Destroy(gameObject,2.0f);
               
            }
            Invoke("BerralMassOrginal",1f);

          
            // ������ٵ� Ŭ���� ���� �Լ��� AddExplosionForce(���ķ�,��ġ, �ݰ�, ���� �ڱ�ġ�� ��)�� �ǹ��Ѵ�.
        }
    }
    void BerralMassOrginal()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, 100.0f);
        // �跲 �ڱ��ڽ� ��ġ���� 20���濡 �跲 ���̾ Cols ��� �迭�� ��´�.

        foreach (Collider coll in colls)
        {
            Rigidbody rigidbody = coll.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                
                rigidbody.mass = 60f;
                

            }
           


           
        }

    }
}
