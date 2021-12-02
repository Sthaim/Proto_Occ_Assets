using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject text;

    public GameObject blackOutSquare;

    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
        blackOutSquare.GetComponent<Image>().color = new Color(blackOutSquare.GetComponent<Image>().color.r, blackOutSquare.GetComponent<Image>().color.g, blackOutSquare.GetComponent<Image>().color.b, 0);
    }

    private void Update()
    {
        
    }

    public void startCoroutine()
    {
        StartCoroutine(FadeBlackOutSquare(true));
    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 1)
    { 

        Color objectColor = blackOutSquare.GetComponent<Image>().color;

        float fadeAmount;

        if (fadeToBlack)
        {
            while (blackOutSquare.GetComponent<Image>().color.a< 1)
            {
                
                fadeAmount = objectColor.a + (fadeSpeed*Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount); 
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
            SceneManager.LoadScene("Tuto");
        } else
        {
            while (blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
                
            }
            SceneManager.LoadScene("Tuto");
        }
    }
    // Update is called once per frame
}
