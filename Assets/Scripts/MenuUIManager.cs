using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour {
    public AudioSource buttonClickedSound;
    public CursorManager cursorManager;
    private float timeToDecreaseVol = 0.2f;
    private void Start()
    {
        PlayerPrefs.DeleteAll();
        //Test Game Use
        //PlayerPrefs.SetInt("LevelCleared", 3);
        Time.timeScale = 1f;
        cursorManager.MouseExit();
    }
    public void QuitGame()
    {
        Debug.Log("I have Quit the game~");
        Application.Quit();
    }

    public void LoadScene(int sceneIndex)
    {
        buttonClickedSound.Play();
        StartCoroutine(DecreaseVolAndLoadScene(sceneIndex));
    }
    IEnumerator DecreaseVolAndLoadScene(int sceneIndex)
    {
        while (buttonClickedSound.isPlaying)
        {
            FindObjectOfType<AudioManager>().GetComponent<AudioSource>().volume = Mathf.Clamp01(timeToDecreaseVol / 0.2f);
            timeToDecreaseVol -= Time.unscaledDeltaTime;
            yield return null;
        }
        if (timeToDecreaseVol < 0f)
        {
            timeToDecreaseVol = 0.2f;
            FindObjectOfType<AudioManager>().RecoverVolume();
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
