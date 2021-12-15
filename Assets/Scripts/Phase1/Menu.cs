using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject blackOutSquare;
    public GameObject go_texteFinDePhase;

    // Start is called before the first frame update
    void Start()
    {
        go_texteFinDePhase.SetActive(false);
        blackOutSquare.GetComponent<Image>().color = new Color(blackOutSquare.GetComponent<Image>().color.r, blackOutSquare.GetComponent<Image>().color.g, blackOutSquare.GetComponent<Image>().color.b, 1);
        startCoroutine(false);
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
                go_texteFinDePhase.SetActive(true);
                GetComponent<Phase1Manager>().tmp_nbrEnemy.color = GetComponent<Phase1Manager>().tmp_nbrEnemy.color = new Color(255, 255, 255, 0);
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
                GetComponent<Phase1Manager>().tmp_nbrEnemy.color = GetComponent<Phase1Manager>().tmp_nbrEnemy.color = new Color(255, 255, 255, 1);
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
