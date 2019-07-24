using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PressBackButton : MonoBehaviour
{
    public Image backButtonImage;
    public AudioSource holdSound;
    public AudioClip[] sounds;
    public CursorManager cursorManager;
    private float percent = 0f;
    private bool isHeld = false, needReverse = false;
    private void Start()
    {
        Time.timeScale = 1f;
        cursorManager.MouseExit();
    }
    public void HoldButton()
    {
        isHeld = true;
        StartCoroutine(LoadBar());
        holdSound.time = Mathf.Clamp01(percent);
        holdSound.clip = sounds[0]; 
        holdSound.Play();
    }
    public void ReleaseButton()
    {
        isHeld = false;
        needReverse = true;
        holdSound.Stop();
        holdSound.clip = sounds[1];
        holdSound.time =holdSound.clip.length - Mathf.Clamp01(percent);
        holdSound.Play();
        StartCoroutine(ReverseBar());
    }

    IEnumerator LoadBar()
    {
        while (isHeld)
        {
            percent += Time.deltaTime;
            backButtonImage.fillAmount = Mathf.Clamp01(percent);
            FindObjectOfType<AudioSource>().volume =1- Mathf.Clamp01(percent);
            if (percent > 0.95f)
            {
                SceneManager.LoadScene(1);
                FindObjectOfType<AudioManager>().RecoverVolume();
            }
            yield return null;
        }
    }

    IEnumerator ReverseBar()
    {
        yield return null;
        while (needReverse && !isHeld)
        {
            percent -= Time.deltaTime;
            backButtonImage.fillAmount = Mathf.Clamp01(percent);
            FindObjectOfType<AudioSource>().volume = 1 - Mathf.Clamp01(percent);
            if (percent < 0f)
            {
                needReverse = false;
            }
            yield return null;
        }
    }
}
