using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ChangeSprite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    private void changeSprite(Sprite _newSprite)
    {
        
        GetComponent<Image>().sprite = _newSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
