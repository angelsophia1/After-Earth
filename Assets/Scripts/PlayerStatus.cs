using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
public class PlayerStatus : MonoBehaviour
{
    public float maxHp = 100;
    public Image HP,oxygenAmount,HPHurtEffect;
    public GameObject hurtEffect,restartMenu,direction,inGameUI;
    public GameObject[] interactionText;
    public bool beingDamaged;
    public AudioSource buttonClickedSource;
    private float currentHp = 100,timeOfImmune = 0.75f, outerRadius = 9f,innerRadius=6.5f,timeOfFullOxygen = 180f,timeOfOxygenLeft ;
    private bool immuneDamage;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHp = maxHp;
        immuneDamage = false;
        beingDamaged = false;
        timeOfOxygenLeft = timeOfFullOxygen;
    }
    private void Update()
    {
        CheckOxygen();
        if (immuneDamage)
        {
            timeOfImmune -= Time.deltaTime;
            if (timeOfImmune<0f)
            {
                timeOfImmune = 0.75f;
                immuneDamage = false;
            }
        }else if (beingDamaged)
        {
            TakeDamage();
        }
        if (HPHurtEffect.fillAmount> HP.fillAmount)
        {
            HPHurtEffect.fillAmount -= Time.deltaTime *0.2f;
        }
        CreateDirectionIfItemInRange();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Item"))
        {
            interactionText[0].GetComponent<TextMeshProUGUI>().text = "Press E to collect";
            if (!interactionText[0].activeSelf)
            {
                interactionText[0].SetActive(true);
            }
        }
        else if (collision.transform.CompareTag("RescueTarget"))
        {
            interactionText[0].GetComponent<TextMeshProUGUI>().text = "Press E to rescue";
            if (!interactionText[0].activeSelf)
            {
                interactionText[0].SetActive(true);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Item"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                buttonClickedSource.Play();
                int id = collision.GetComponent<Item>().itemData.id;
                string itemName = collision.GetComponent<Item>().itemData.itemName;
                Sprite itemSprite = collision.GetComponent<Item>().itemData.itemSprite;
                if (FindObjectOfType<Inventory>().AddItem(id, itemName, itemSprite))
                {
                    interactionText[0].SetActive(false);
                    Destroy(collision.gameObject);
                }
                else
                {
                    interactionText[0].SetActive(false);
                    interactionText[1].SetActive(true);
                }
            }
        }
        else if (collision.transform.CompareTag("Enemy"))
        {
            beingDamaged = true;
        }
        else if (collision.transform.CompareTag("RescueTarget"))
        {
            if (Input.GetKey(KeyCode.E))
            {
                interactionText[0].SetActive(false);
                buttonClickedSource.Play();
                FindObjectOfType<GameUIManager>().ShowCongrats();
                PlayerPrefs.SetInt("LevelCleared", 2);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (new string[] { "Item", "RescueTarget" }.Contains(collision.transform.tag))
        {
            if (interactionText[0].activeSelf)
            {
                interactionText[0].SetActive(false);
            }else if (interactionText[1].activeSelf)
            {
                interactionText[1].SetActive(false);
            }
        }
        else if (collision.transform.CompareTag("Enemy"))
        {
            StopBeingDamaged();
        }
        //else if (collision.transform.CompareTag("RescueTarget"))
        //{
        //    if (interactionText[0].activeSelf)
        //    {
        //        interactionText[0].SetActive(false);
        //    }
        //}
    }
    public void TakeDamage()
    {
        if (!immuneDamage)
        {
            Instantiate(hurtEffect,transform.position,Quaternion.identity);
            audioSource.Play();
            float damage = Random.Range(5, 15);
            FindObjectOfType<GameUIManager>().InstantiateDamageNumber("-"+damage.ToString());
            currentHp -= damage;
            HP.fillAmount = currentHp / maxHp;
            if (currentHp <= 0)
            {
                Restart();
            }
            immuneDamage = true;
        }
    }
    public void StopBeingDamaged()
    {
        beingDamaged = false;
        immuneDamage = false;
        timeOfImmune = 0.75f;
    }
    private void CreateDirectionIfItemInRange()
    {
        int layerMask = 1 << 8;
        List<Collider2D> outer = new List<Collider2D>(Physics2D.OverlapCircleAll(transform.position, outerRadius,layerMask));
        Collider2D[] inner = Physics2D.OverlapCircleAll(transform.position, innerRadius,layerMask);
        foreach (Collider2D collider in inner)
        {
            outer.Remove(collider);
        }
        foreach (Collider2D collider in outer)
        {
            if ((collider.transform.position-transform.position).magnitude> innerRadius && !collider.GetComponent<Item>().directed)
            {
                GameObject directionClone = Instantiate(direction, Vector3.zero, Quaternion.identity,inGameUI.transform);
                directionClone.GetComponent<ItemDirection>().target = collider.transform;
                directionClone.GetComponent<ItemDirection>().imageToShow = collider.GetComponent<Item>().itemData.itemSprite;
                collider.GetComponent<Item>().directed = true;
            }
        }
    }
    private void CheckOxygen()
    {
        timeOfOxygenLeft -= Time.deltaTime;
        if (timeOfOxygenLeft < 0)
        {
            Restart();
        }
        oxygenAmount.fillAmount = Mathf.Clamp01(timeOfOxygenLeft / timeOfFullOxygen);
    }
    private void Restart()
    {
        Time.timeScale = 0f;
        Weapon.canFire = false;
        restartMenu.SetActive(true);
    }
}