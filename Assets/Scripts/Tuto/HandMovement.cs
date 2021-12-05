using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    public Animator a_handAnim;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        handSelect();
    }

    public void handAppear()
    {
        gameObject.SetActive(true);
    }

    public void handSelect()
    {
        a_handAnim.SetTrigger("isSelecting");
    }

    public void handGlisse()
    {
        a_handAnim.SetTrigger("isGlisse");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
