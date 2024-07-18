using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPullingManger : MonoBehaviour
{
    public static ObjectPullingManger pullingManger;
    [SerializeField]
    
    private GameObject bullet; // 
    private GameObject E_bullet;
    public int maxPool = 10;// ������Ʈ Ǯ�� �� ����
    // List � �ڷ����� ���� ���� �� ���� �� �迭�� ���� ��°�
    public List<GameObject> bulletPoolList;
    public List <GameObject> E_bulletPoolList;
    private void Awake() // Start,Awake,OnEnable �ٰ��� �������ڸ��� ȣ�� 
    {
        if (pullingManger == null)
        {
            pullingManger = this;
        }
        else if (pullingManger != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        bullet = Resources.Load("Bullet") as GameObject;
        E_bullet = Resources.Load("EnBullt") as GameObject;
        CreateBulletPool();
        CrateBulletPoolEnemy();
    }
    void CreateBulletPool()
    {
        // ���� ������Ʈ ����
        GameObject playerBulletGroup = new GameObject("PlayerBulletGroup");
        for( int i = 0; i< 10; i++ )
        {
            var _bullet = Instantiate(bullet,playerBulletGroup.transform);
            _bullet.name = $"{(i+1).ToString()}��";
            _bullet.SetActive(false);
            bulletPoolList.Add(_bullet);
        }
    }
    void CrateBulletPoolEnemy()
    {
        GameObject EnemyBulletGroup = new GameObject("EnemyGroup");
        for (int i = 0; i < 10; i++)
        {
            var _bullet2 = Instantiate(E_bullet, EnemyBulletGroup.transform);
            _bullet2.name = $"{(i + 1).ToString()}��";
            _bullet2.SetActive(false);
            E_bulletPoolList.Add(_bullet2);
        }
    }
  
    public GameObject CreatEnBulletPool()
    {
        for (int i = 0; i< E_bulletPoolList.Count; i ++ )
        {
            if ( E_bulletPoolList[i].activeSelf == false)
            {
                return E_bulletPoolList[i];
            }
        }
        return null;
    }
    public GameObject GetBulletPool()
    {
        for(int i = 0; i< bulletPoolList.Count; i++ )
        {
            // ��Ȱ�� �Ǿ��ٸ� activeSelf�� Ȱ��ȭ ��Ȱ�� ���θ� �˷���
            if (bulletPoolList[i].activeSelf==false)
            {
                return bulletPoolList[i];
            }
        }
        return null;
    }
}
