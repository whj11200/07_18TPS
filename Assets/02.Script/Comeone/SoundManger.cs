using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    public static SoundManger S_Instance;
    public float SoundVoumn = 1.0f; // ���� ����
    public bool isSoundMute = false; // ���Ұ�
    public Transform tr;

    void Awake()
    {
        if(S_Instance == null)
        {
            S_Instance = this;

        }
        else if (S_Instance != this)
        {
            Destroy(S_Instance);
        } 
        DontDestroyOnLoad(gameObject);
    }
    public void PlaySound(Vector3 pos, AudioClip cilp)
    {
        if (isSoundMute)
        {
            return; // ���Ұ� �϶� �� �Լ��� ����������.
        }

        GameObject soundObject = new GameObject("Sound!");
        // ������Ʈ ���� ����
        soundObject.transform.position = pos; // �Ҹ��� ��ġ�� ���� ����
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        // ������ҽ� �� ���� AddComponent<AudioSource>�� ����� �ҽ��� �ִ´�.
        
        audioSource.clip = cilp;
        audioSource.minDistance = 20f;
        audioSource.maxDistance = 50f;
        audioSource.volume = SoundVoumn;
        audioSource.Play();
        Destroy(soundObject,cilp.length);
    }

    
    void Update()
    {
        
    }
}
