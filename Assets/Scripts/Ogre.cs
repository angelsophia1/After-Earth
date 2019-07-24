using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ogre : Enemy
{
    private Transform player;
    private Rigidbody2D rigidbody2D;
    private float turnTime = 2f,maxRadius = 2.5f,smoothTime = 1.5f;
    private Transform child;
    private bool faceRight,chasePlayer;
    private Vector3 smoothPosition, velocity = new Vector3(0.5f, 0f,0f);
    private void Start()
    {
        faceRight = true;
        chasePlayer = false;
        rigidbody2D = GetComponent<Rigidbody2D>();
        child = transform.GetChild(0);
        player = FindObjectOfType<PlayerMovement>().transform;
    }
    private void FixedUpdate()
    {
        if (!chasePlayer)
        {
            chasePlayer =IfInCircle();
            if (turnTime < 0f)
            {
                turnTime = 2f;
                Flip();
            }
            turnTime -= Time.deltaTime;
            if (faceRight)
            {
                rigidbody2D.velocity = velocity;
            }
            else
            {
                rigidbody2D.velocity = -velocity;
            }
        }
        else
        {
            if (IfRightOfPlayer())
            {
                if (!faceRight)
                {
                    Flip();
                }
            }
            else
            {
                if (faceRight)
                {
                    Flip();
                }
            }
            smoothPosition = Vector3.SmoothDamp(transform.position,player.position,ref velocity, smoothTime);
            transform.position = smoothPosition ;
        }       
    }
    private bool IfRightOfPlayer()
    {
        if ((player.position-transform.position).x<0f)
        {
            return false;
        }
        return true;
    }
    private void Flip()
    {
        transform.Rotate(0f,180f,0f);
        child.GetComponent<RectTransform>().Rotate(0f, 180f, 0f);
        faceRight = !faceRight;
    }
    private bool IfInCircle()
    {
        Collider2D[] overlaps = new Collider2D[10];
        int layerMask = 1 << 2;//Player
        int count = Physics2D.OverlapCircleNonAlloc(transform.position,maxRadius,overlaps,layerMask);
        for (int i = 0; i < count; i++)
        {
            if (overlaps[i] != null)
            {
                rigidbody2D.velocity = Vector3.zero;
                velocity = Vector3.zero;
                return true;
            }
        }
        return false;
    }
}
