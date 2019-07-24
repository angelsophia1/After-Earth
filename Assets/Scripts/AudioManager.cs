using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class AudioManager : MonoBehaviour {
    public AudioClip[] audioClips;
    public static AudioManager instance;
    private AudioSource audioSource;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else if(instance !=null && instance !=this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }
    public void PlayBGMByScene(int sceneIndex)
    {
        audioSource.volume = 1f;
        //2 BGM: 1 Menu, 1 In Game
        if (sceneIndex <6)
        {
            PlayClip(0);
        }else 
        {
            PlayClip(1);
        }
        //switch (sceneIndex)
        //{
        //    case 0:
        //        PlayClip(0);
        //        break;
        //    case 1:
        //        PlayClip(0);
        //        break;
        //    case 2:
        //        PlayClip(0);
        //        break;
        //    case 3:
        //        PlayClip(0);
        //        break;
        //    case 4:
        //        PlayClip(0);
        //        break;
        //    case 5:
        //        PlayClip(0);
        //        break;
        //    case 6:
        //        PlayClip(1);
        //        break;
        //    case 7:
        //        PlayClip(1);
        //        break;
        //}
    }
    public void PlayClip(int index)
    {
        RecoverVolume();
        audioSource.clip = audioClips[index];
        audioSource.Play();
    }
    public void RecoverVolume()
    {
        StartCoroutine(WaitRecoverVolume());
    }
    IEnumerator WaitRecoverVolume()
    {
        while (audioSource.volume < 1f)
        {
            audioSource.volume += 0.1f;
            yield return null;
        }
        audioSource.volume = 1f;
    }
}
