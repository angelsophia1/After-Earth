using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemDirection : MonoBehaviour
{
    public float uiXRange, uiYRange, cameraXRange, cameraYRange;
    public Image image;
    //target needs to be specified while instantiating
    public Transform target,player;
    //imageToShow needs to be specified while instantiating
    public Sprite imageToShow;
    [SerializeField]
    private float offset,showRange,disapearRange;
    private Vector3 positionToBe,difference;
    private RectTransform rectTransform;
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        rectTransform = GetComponent<RectTransform>();
        image.sprite = imageToShow;
        ChangePositionn();
    }
    // Update is called once per frame
    void Update()
    {
        ChangePositionn();
    }
    private void ChangePositionn()
    {
        if (target != null)
        {
            difference = target.position - player.position;
            if (difference.magnitude > showRange && difference.magnitude < disapearRange)
            {
                float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                positionToBe.x = Mathf.Clamp((difference.x) / cameraXRange * uiXRange, -uiXRange, uiXRange);
                positionToBe.y = Mathf.Clamp((difference.y) / cameraYRange * uiYRange, -uiYRange, uiYRange);
                positionToBe.z = 0f;
                rectTransform.localPosition = positionToBe;
                rectTransform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
            }
            else
            {
                target.GetComponent<Item>().directed = false;
                Destroy(gameObject);
            }
        }
    }
}
