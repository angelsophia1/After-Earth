using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveH, moveV;
    [SerializeField]
    private float moveSpeed , transitionSpeed =20f,floatRange = 1f;
    private Vector3 offset,newTransformToBe;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        newTransformToBe = transform.position;
    }
    private void Start()
    {
        InvokeRepeating("FloatingAround", 0f,0.5f);
    }
    private void Update()
    {
        moveH = Input.GetAxis("Horizontal") * moveSpeed *Time.deltaTime;
        moveV = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position,newTransformToBe,Time.deltaTime*transitionSpeed);
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveH, moveV);
    }
    private void FloatingAround()
    {
        offset = new Vector3(Random.Range(-floatRange, floatRange), Random.Range(-floatRange, floatRange), 0f) * 3f;
        newTransformToBe=transform.position + offset;
    }
}

