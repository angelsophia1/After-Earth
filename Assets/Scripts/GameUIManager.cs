using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameUIManager : MonoBehaviour
{
    public CursorManager cursorManager;
    public GameObject pauseMenuUI,damagePopUpPrefab,congratulationsMenu;
    private AudioSource buttonClickedSource;
    private bool gameIsPaused = false;
    private float timeToDecreaseVol = 0.2f;
    private Vector3 offset = new Vector3(0f,0f,0f);
    private void Start()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        cursorManager.MouseExit();
        buttonClickedSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseButtonClicked();
        }

    }
    public void PauseButtonClicked()
    {
        if (gameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
    public void Resume()
    {
        cursorManager.MouseExit();
        buttonClickedSource.Play();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        gameIsPaused = false;
        Weapon.canFire = true;
    }

    public void Pause()
    {
        buttonClickedSource.Play();
        Weapon.canFire = false;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        gameIsPaused = true;
    }

    public void BackToMenu()
    {
        //Resume();
        buttonClickedSource.Play();
        StartCoroutine(DecreaseVolAndLoadScene(1));
    }
    public void Restart()
    {
        buttonClickedSource.Play();
        StartCoroutine(DecreaseVolAndLoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void InstantiateDamageNumber(string damageNumber)
    {
        GameObject damagePopUp =Instantiate(damagePopUpPrefab, transform.position+offset, Quaternion.identity,transform);
        damagePopUp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = damageNumber;
    }
    IEnumerator DecreaseVolAndLoadScene(int sceneIndex)
    {
        while (buttonClickedSource.isPlaying)
        {
            FindObjectOfType<AudioManager>().GetComponent<AudioSource>().volume = Mathf.Clamp01(timeToDecreaseVol / 0.2f);
            timeToDecreaseVol -= 0.05f;
            yield return new WaitForSecondsRealtime(0.05f);
        }
        if (timeToDecreaseVol < 0f)
        {
            timeToDecreaseVol = 0.2f;
            FindObjectOfType<AudioManager>().PlayBGMByScene(sceneIndex);
            SceneManager.LoadScene(sceneIndex);
        }
    }
    public void CheckIfGameClear()
    {
            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 7:
                    if (FindObjectOfType<Inventory>().CheckIfGameClear())
                    {
                        ShowCongrats();
                    }
                    PlayerPrefs.SetInt("LevelCleared", 1);
                    break;
                //case 8:
                //    ShowCongrats();
                //    PlayerPrefs.SetInt("LevelCleared", 2);
                //    break;
                case 11:
                    if (FindObjectOfType<EnemyKilled>().CheckIfGameClear())
                    {
                        ShowCongrats();
                    }
                    PlayerPrefs.SetInt("LevelCleared", 3);
                    break;
        }
        //if (FindObjectOfType<EnemyKilled>().CheckIfGameClear())
        //if (FindObjectOfType<Inventory>().CheckIfGameClear())
        //if (FindObjectOfType<Inventory>().CheckIfGameClear() && FindObjectOfType<EnemyKilled>().CheckIfGameClear())
        //{
        //Weapon.canFire = false;
        //Time.timeScale = 0f;
        //congratulationsMenu.SetActive(true);
        //}
    }
    public void ShowCongrats()
    {
        Weapon.canFire = false;
        Time.timeScale = 0f;
        congratulationsMenu.SetActive(true);
    }
    public void LevelCleared(int index)
    {
        buttonClickedSource.Play();
        StartCoroutine(DecreaseVolAndLoadScene(index));
    }
}
