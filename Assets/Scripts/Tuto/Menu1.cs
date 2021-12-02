using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu1 : MonoBehaviour
{

    public GameObject blackOutSquare;

    public TextMeshProUGUI firstText;
    public TextMeshProUGUI secondText;
    public TextMeshProUGUI thirdText;

    public delegate IEnumerator TestDelegate(bool test, int Speed); // This defines what type of method you're going to call.
    public TestDelegate BlackFade;
    public TestDelegate firstTextAppear;
    public TestDelegate secondTextAppear;
    public TestDelegate thirdTextAppear;

    public bool finishedAppear;

    // Start is called before the first frame update
    void Start()
    {
        finishedAppear = false;
        firstText.color = new Color(firstText.color.r, firstText.color.g, firstText.color.b, 0);
        secondText.color = new Color(firstText.color.r, firstText.color.g, firstText.color.b, 0);
        thirdText.color = new Color(firstText.color.r, firstText.color.g, firstText.color.b, 0);
        blackOutSquare.GetComponent<Image>().color = new Color(blackOutSquare.GetComponent<Image>().color.r, blackOutSquare.GetComponent<Image>().color.g, blackOutSquare.GetComponent<Image>().color.b, 1);
        BlackFade = FadeBlackOutSquare;
        firstTextAppear = TutoTextAppear;
        secondTextAppear = TutoText2Appear;
        thirdTextAppear = TutoText3Appear;
        startCoroutine(BlackFade,false,1);
        startCoroutine(firstTextAppear, true, 1);
        startCoroutine(secondTextAppear, true, 1);
    }

    private void Update()
    {
        
    }

    public void startCoroutine(TestDelegate functionTest, bool appear, int vitesse)
    {
        StartCoroutine(functionTest(appear, vitesse));
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
        } else
        {
            while (blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
    }

    private IEnumerator TutoTextAppear (bool fadeToExsistence = true, int fadeSpeed = 1)
    {
        Color objectColor = firstText.color;

        float fadeAmount;

        

        if (fadeToExsistence)
        {
            yield return new WaitForSeconds(2.5f);
            while (firstText.color.a < 1)
            {

                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                firstText.color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (firstText.color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                firstText.color = objectColor;
                yield return null;
            }
        }
    }

    private IEnumerator TutoText2Appear(bool fadeToExsistence = true, int fadeSpeed = 1)
    {
        Color objectColor = secondText.color;

        float fadeAmount;

        

        if (fadeToExsistence)
        {
            yield return new WaitForSeconds(5f);
            while (secondText.color.a < 1)
            {

                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                secondText.color = objectColor;
                yield return null;
            }
            finishedAppear = true;
        }
        else
        {
            while (secondText.color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                secondText.color = objectColor;
                yield return null;
            }
        }
    }

    private IEnumerator TutoText3Appear(bool fadeToExsistence = true, int fadeSpeed = 1)
    {
        Color objectColor = thirdText.color;

        float fadeAmount;

        if (fadeToExsistence)
        {
            yield return new WaitForSeconds(2f);
            while (thirdText.color.a < 1)
            {

                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                thirdText.color = objectColor;
                yield return finishedAppear = true;
            }
        }
        else
        {
            yield return new WaitForSeconds(2f);
            while (thirdText.color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                thirdText.color = objectColor;
                yield return null;
            }
        }
    }
    // Update is called once per frame
}
