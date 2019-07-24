using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Enemy : MonoBehaviour
{
    public CursorManager cursorManager;
    public GameObject damagePopUpPrefab,meat;
    public Image enemyHP,enemyHPHurtEffect;
    public int enemyID;// 0 for ogre, 1 for piranha
    private bool isMouseOver;
    private float maxHP = 30f,currentHP;
    private void Awake()
    {
        currentHP = maxHP;
        isMouseOver = false;
    }
    private void Update()
    {
        if (enemyHPHurtEffect.fillAmount > enemyHP.fillAmount)
        {
            enemyHPHurtEffect.fillAmount -= Time.deltaTime * 0.8f;
        }else if (enemyHPHurtEffect.fillAmount<0.1f)
        {
            FindObjectOfType<EnemyKilled>().AddNumber(enemyID);
            if (FindObjectOfType<PlayerStatus>().beingDamaged)
            {
                FindObjectOfType<PlayerStatus>().StopBeingDamaged();
            }
            Instantiate(meat, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void OnMouseEnter()
    {
        cursorManager.MouseEnterEnemy();
    }
    private void OnMouseOver()
    {
        isMouseOver = true;
    }
    private void OnMouseExit()
    {
        cursorManager.MouseExit();
        isMouseOver = false;
    }
    private void OnDestroy()
    {
        if (isMouseOver)
        {
            cursorManager.MouseExit();
        }
    }
    public void TakeDamage()
    {
        float damage = Random.Range(5,15);
        currentHP -= damage;
        enemyHP.fillAmount = currentHP / maxHP;
        GameObject damagePopUp = Instantiate(damagePopUpPrefab,gameObject.transform.GetChild(0).position,Quaternion.identity, gameObject.transform.GetChild(0));
        damagePopUp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "-" + damage.ToString();
        //if (currentHP<0.1f)
        //{
        //    FindObjectOfType<EnemyKilled>().AddNumber(enemyID);
        //    if (FindObjectOfType<PlayerStatus>().beingDamaged)
        //    {
        //        FindObjectOfType<PlayerStatus>().StopBeingDamaged();
        //    }
        //    Instantiate(meat,transform.position,Quaternion.identity);
        //    Destroy(gameObject);
        //}
    }
}
