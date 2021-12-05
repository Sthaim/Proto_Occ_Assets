using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu1 : MonoBehaviour
{

    public GameObject blackOutSquare;

    public string[] myTextesTop;
    public int indexMyTextesTop;
    public string[] myTextesBottom;
    public int indexMyTextesBottom;
    public TextMeshProUGUI myTextMeshTop;
    public TextMeshProUGUI myTextMeshBottom;
    public GameObject Enemy;
    public GameObject go_hand;

    public enum Positions { NULL = 0, TOP = 1, BOTTOM = 2 }

    public delegate IEnumerator TestDelegate(bool test, int Speed); // This defines what type of method you're going to call.
    public TestDelegate BlackFade;

    public bool finishedAppear;

    // Start is called before the first frame update
    void Start()
    {
        Enemy.SetActive(false);
        finishedAppear = false;
        myTextMeshTop.color = new Color(myTextMeshTop.color.r, myTextMeshTop.color.g, myTextMeshTop.color.b, 0);
        myTextMeshBottom.color = new Color(myTextMeshBottom.color.r, myTextMeshBottom.color.g, myTextMeshBottom.color.b, 0.01f);
        blackOutSquare.GetComponent<Image>().color = new Color(blackOutSquare.GetComponent<Image>().color.r, blackOutSquare.GetComponent<Image>().color.g, blackOutSquare.GetComponent<Image>().color.b, 1);
        StartCoroutine(FadeBlackOutSquare(false));

        // TEXT TOP
        // On place le texte top
        SetTextOnTMP(myTextMeshTop, Positions.TOP);
        // On affiche le texte top
        DelayText(myTextMeshTop, 0.01f);

        // TEXT BOTTOM
        // On place le texte top
        SetTextOnTMP(myTextMeshBottom, Positions.BOTTOM);
        // On affiche le texte top
        DelayText(myTextMeshBottom, 0.01f,2);

    }

    private void Update()
    {
        
    }


    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = false, int fadeSpeed = 1)
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
            SceneManager.LoadScene("Phase1");
        } else
        {
            while (blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
                go_hand.GetComponent<HandMovement>().handAppear();
                go_hand.GetComponent<HandMovement>().handSelect();
            }
        }
    }

    // SET TEXT
    public void SetTextOnTMP(TextMeshProUGUI _TMP, Positions _pos)
    {
        if (_pos == Positions.TOP)
        {
            if (indexMyTextesTop < myTextesTop.Length)
            {
                _TMP.text = myTextesTop[indexMyTextesTop];
                indexMyTextesTop++;
            }
        }

        else if (_pos == Positions.BOTTOM)
        {
            if (indexMyTextesBottom < myTextesBottom.Length)
            {
                _TMP.text = myTextesBottom[indexMyTextesBottom];
                indexMyTextesBottom++;
            }
        }
    }

    // FADE
    public void FadeInText(TextMeshProUGUI _TMP, float _s = 1f)
    {
        StartCoroutine(FadeInTextCoroutine(_TMP, _s));
    }
    public void FadeOutText(TextMeshProUGUI _TMP, float _s = 1f)
    {
        StartCoroutine(FadeOutTextCoroutine(_TMP, _s));
    }

    IEnumerator FadeInTextCoroutine(TextMeshProUGUI _TMP, float _s)
    {
        yield return new WaitForSeconds(_s);
        print("FadeIn");
        // Ajout de l'alpha au texte
        _TMP.color = new Color(_TMP.color.r, _TMP.color.g, _TMP.color.b, _TMP.color.a + 0.01f);
        if (_TMP.color.a >= 0.95)
        {
            print("Stop money");
            StopCoroutine(FadeInTextCoroutine(_TMP, _s));
            _TMP.color = new Color(_TMP.color.r, _TMP.color.g, _TMP.color.b, 1);
        }
        else
        {
            StartCoroutine(FadeInTextCoroutine(_TMP, _s));
        }
    }
    IEnumerator FadeOutTextCoroutine(TextMeshProUGUI _TMP, float _s)
    {
        yield return new WaitForSeconds(_s);
        print("FadeOut");
        // Ajout de l'alpha au texte
        _TMP.color = new Color(_TMP.color.r, _TMP.color.g, _TMP.color.b, _TMP.color.a - 0.05f);
        if (_TMP.color.a <= 0)
        {
            print("Stop money Out");
            StopCoroutine(FadeOutTextCoroutine(_TMP, _s));
            _TMP.color = new Color(_TMP.color.r, _TMP.color.g, _TMP.color.b, 0);
        }
        else
        {
            StartCoroutine(FadeOutTextCoroutine(_TMP, _s));
        }
    }
    public void DelayText(TextMeshProUGUI _TMP, float _s, float _delay = 1, bool _fadeIn = true)
    {
        StartCoroutine(DelayTextCoroutine(_TMP, _s, _delay, _fadeIn));
    }
    IEnumerator DelayTextCoroutine(TextMeshProUGUI _TMP, float _s, float _delay = 1, bool _fadeIn = true)
    {
        yield return new WaitForSeconds(_delay);
        if (_fadeIn)
        {
            FadeInText(_TMP, _s);
        }
        else
        {
            FadeOutText(_TMP, _s);
        }
    }
}
