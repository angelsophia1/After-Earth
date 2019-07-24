using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemyKilled : MonoBehaviour
{
    public TextMeshProUGUI[] numberText;
    public int[] taskNumber = new int[2];
    private int[] number = new int[2] { 0,0};
    private void Start()
    {
        for (int i = 0; i < number.Length; i++)
        {
            number[i] = 0;
        }
    }
    public void AddNumber(int enemyID)
    {
        number[enemyID]++;
        numberText[enemyID].text = number[enemyID].ToString();
        FindObjectOfType<GameUIManager>().CheckIfGameClear();
    }
    public bool CheckIfGameClear()
    {
        for (int i = 0; i < number.Length; i++)
        {
            if (number[i] <taskNumber[i])
            {
                return false;
            }
        }
        return true;
    }
}
