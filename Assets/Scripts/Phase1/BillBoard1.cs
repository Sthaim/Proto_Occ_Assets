using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillBoard1 : MonoBehaviour
{
    public Camera targetCamera;
    private Vector3 angleOffset;
    public GameObject go_selectedSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(targetCamera.transform); 
        if (GetComponent<SpriteRenderer>().material.color == Color.blue)
        {
            GetComponent<SpriteRenderer>().material.color = Color.white;
            go_selectedSprite.SetActive(true);
            go_selectedSprite.GetComponent<Image>().sprite = GetComponent<SpriteRenderer>().sprite;
        }
        
            

    }
}
