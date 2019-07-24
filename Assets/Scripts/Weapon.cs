using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public static bool canFire;
    public GameObject[] projectilePrefab;
    public Transform firePosition;
    public AudioSource audiosource;
    public Sprite[] directionSprites;// 0 right,1 down,2 left, 3 up;
    private SpriteRenderer spriteRenderer;
    private int faceDirection;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        canFire = true;
        faceDirection = 0;// 0 right,1 down,2 left, 3 up;
    }
    private void Update()
    {
        WeaponRotation();

        if (Input.GetMouseButton(0)&&canFire)
        {
            Fire();
        }
    }
    private void WeaponRotation()
    {
        if (Time.timeScale >0.1f)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            //right
            if (rotZ<45 &&rotZ>-45)
            {
                if (faceDirection !=0 )
                {
                    if (faceDirection == 2)
                    {
                        transform.GetChild(0).localRotation = Quaternion.Euler(0f, 0f, 0f);
                    }
                    faceDirection = 0;
                    spriteRenderer.sprite = directionSprites[0];
                }
            }
            //down
            else if (rotZ < -45 && rotZ >-135)
            {
                //down
                if (faceDirection != 1)
                {
                    if (faceDirection == 2)
                    {
                        transform.GetChild(0).localRotation = Quaternion.Euler(0f, 0f, 0f);
                    }
                    faceDirection = 1;
                    spriteRenderer.sprite = directionSprites[1];
                }
            }
            //left
            else if (rotZ<-135 || rotZ >135)
            {
                //left
                if (faceDirection != 2)
                {
                    faceDirection = 2;
                    spriteRenderer.sprite = directionSprites[2];
                    transform.GetChild(0).localRotation = Quaternion.Euler(0f, -180f, 180f);
                }
            }
            //up
            else if (rotZ < 135 && rotZ>45)
            {
                //up
                if (faceDirection != 3)
                {
                    if (faceDirection == 2)
                    {
                        transform.GetChild(0).localRotation = Quaternion.Euler(0f, 0f, 0f);
                    }
                    faceDirection = 3;
                    spriteRenderer.sprite = directionSprites[3];
                }
            }
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
    }
    private void Fire()
    {
        int randomIndex = Random.Range(0,6);
        Instantiate(projectilePrefab[randomIndex], firePosition.position, transform.rotation);
        audiosource.Play();
        StartCoroutine(FireCoolDown());
    }
    IEnumerator FireCoolDown()
    {
        canFire = false;
        yield return new WaitForSeconds(0.3f);
        canFire = true;
    }

}
