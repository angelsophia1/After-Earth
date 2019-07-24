using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelUIManager : MonoBehaviour
{
    public AudioSource buttonClickedSound;
    public CursorManager cursorManager;
    public Button[] buttonsToDisable;
    private float timeToDecreaseVol = 0.2f;
    private void Start()
    {
        for (int i = PlayerPrefs.GetInt("LevelCleared", 0); i < buttonsToDisable.Length; i++)
        {
            buttonsToDisable[i].interactable = false;
        }
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
