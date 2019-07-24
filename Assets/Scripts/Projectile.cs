using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject hitEffect;
    [SerializeField]
    private float moveSpeed = 0.1f;
    private void Start()
    {
        StartCoroutine(WaitToDestroy());
    }
    private void Update()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            //other.GetComponent<Enemy>().CheckPlayerStatus();
            //Destroy(other.gameObject);
            other.GetComponent<Enemy>().TakeDamage();
            Instantiate(hitEffect, other.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
    }
}
