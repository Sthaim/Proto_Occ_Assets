using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuDep : MonoBehaviour
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

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = false, float fadeSpeed = 0.01f)
    { 

        Color objectColor = blackOutSquare.GetComponent<Image>().color;


        if (fadeToBlack)
        {
            yield return new WaitForSeconds(0.01f);
            blackOutSquare.GetComponent<Image>().color = new Color(blackOutSquare.GetComponent<Image>().color.r, blackOutSquare.GetComponent<Image>().color.g, blackOutSquare.GetComponent<Image>().color.b, blackOutSquare.GetComponent<Image>().color.a + 0.01f);
            if (blackOutSquare.GetComponent<Image>().color.a > 1)
            {
                
                StopCoroutine(FadeBlackOutSquare(false, 0.01f));
                blackOutSquare.GetComponent<Image>().color = new Color(blackOutSquare.GetComponent<Image>().color.r, blackOutSquare.GetComponent<Image>().color.g, blackOutSquare.GetComponent<Image>().color.b, 1);
                yield return new WaitForSeconds(2);
                SceneManager.LoadScene("Tuto");
            }
            else
            {
                print("oui");
                StartCoroutine(FadeBlackOutSquare(true, 0.01f));
            }

        } else
        {
            yield return new WaitForSeconds(0.01f);
            blackOutSquare.GetComponent<Image>().color = new Color(blackOutSquare.GetComponent<Image>().color.r, blackOutSquare.GetComponent<Image>().color.g, blackOutSquare.GetComponent<Image>().color.b, blackOutSquare.GetComponent<Image>().color.a - 0.01f);
            if (blackOutSquare.GetComponent<Image>().color.a < 0)
            {
                StopCoroutine(FadeBlackOutSquare(false, 0.01f));
                blackOutSquare.GetComponent<Image>().color = new Color(blackOutSquare.GetComponent<Image>().color.r, blackOutSquare.GetComponent<Image>().color.g, blackOutSquare.GetComponent<Image>().color.b, 0);
                yield return new WaitForSeconds(3);
                SceneManager.LoadScene("Tuto");
            }
            else {
                StartCoroutine(FadeBlackOutSquare(false, 0.01f));
            
            }
        }
    }
    // Update is called once per frame
}
