using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuPhase2 : MonoBehaviour
{
    public GameObject blackOutSquare;
    public GameObject go_secondTexte;

    // Start is called before the first frame update
    void Start()
    {
        blackOutSquare.GetComponent<Image>().color = new Color(blackOutSquare.GetComponent<Image>().color.r, blackOutSquare.GetComponent<Image>().color.g, blackOutSquare.GetComponent<Image>().color.b, 1);
        startCoroutine(false);
        go_secondTexte.SetActive(false);
        
        
    }

    private void Update()
    {
        
    }

    public void startCoroutine(bool _fadeToBlack)
    {
        StartCoroutine(FadeBlackOutSquare(_fadeToBlack));
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
                go_secondTexte.SetActive(true);
                GetComponent<Phase2Manager>().tmp_nbrSauve.color = GetComponent<Phase2Manager>().tmp_nbrSauve.color = new Color(255, 255, 255, 0);
            }
            else
            {
                print("oui");
                StartCoroutine(FadeBlackOutSquare(true, 0.01f));
            }

        }
        else
        {
            yield return new WaitForSeconds(0.01f);
            blackOutSquare.GetComponent<Image>().color = new Color(blackOutSquare.GetComponent<Image>().color.r, blackOutSquare.GetComponent<Image>().color.g, blackOutSquare.GetComponent<Image>().color.b, blackOutSquare.GetComponent<Image>().color.a - 0.01f);
            if (blackOutSquare.GetComponent<Image>().color.a < 0)
            {
                StopCoroutine(FadeBlackOutSquare(false, 0.01f));
                blackOutSquare.GetComponent<Image>().color = new Color(blackOutSquare.GetComponent<Image>().color.r, blackOutSquare.GetComponent<Image>().color.g, blackOutSquare.GetComponent<Image>().color.b, 0);
                yield return new WaitForSeconds(3);
                GetComponent<Phase2Manager>().tmp_nbrSauve.color = GetComponent<Phase2Manager>().tmp_nbrSauve.color = new Color(255, 255, 255, 1);
                gameObject.GetComponent<SoundManager>().PlaySound(1);
            }
            else
            {
                StartCoroutine(FadeBlackOutSquare(false, 0.01f));

            }
        }
    }
    // Update is called once per frame
}
