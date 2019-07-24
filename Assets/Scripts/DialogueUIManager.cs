using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class DialogueUIManager : MonoBehaviour
{
    public CursorManager cursorManager;
    public TextMeshProUGUI tmpText;
    public string[] sentencesToDisplay;
    private AudioSource buttonClickedSource;
    private float timeToDecreaseVol = 0.2f;
    private void Start()
    {
        buttonClickedSource = GetComponent<AudioSource>();
        StartCoroutine(DisplayWordByWord(sentencesToDisplay));
        Time.timeScale = 1f;
        cursorManager.MouseExit();
    } 
    public void Continue(int sceneIndex)
    {
        buttonClickedSource.Play();
        StartCoroutine(DecreaseVolAndLoadScene(sceneIndex));
    }
    IEnumerator DecreaseVolAndLoadScene(int sceneIndex)
    {
        while (buttonClickedSource.isPlaying)
        {
            FindObjectOfType<AudioManager>().GetComponent<AudioSource>().volume = Mathf.Clamp01(timeToDecreaseVol / 0.2f);
            timeToDecreaseVol -= Time.unscaledDeltaTime;
            yield return null;
        }
        if (timeToDecreaseVol < 0f)
        {
            timeToDecreaseVol = 0.2f;
            FindObjectOfType<AudioManager>().PlayBGMByScene(SceneManager.GetActiveScene().buildIndex+1);
            SceneManager.LoadScene(sceneIndex);
        }
    }
    IEnumerator DisplayWordByWord(string[] sentence)
    {
        tmpText.text = "";
        yield return null;
        for (int i = 0; i < sentence.Length; i++)
        {
            foreach (char letter in sentence[i].ToCharArray())
            {
                tmpText.text += letter;
                yield return null;
            }
            tmpText.text += "\n";
        }
    }
}
