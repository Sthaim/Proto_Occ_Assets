using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Phase2Manager : MonoBehaviour
{
    private int n_SauveAllie;
    public GameObject go_menu;
    public TextMeshProUGUI tmp_nbrSauve;
    
    private int n_nbrScore;
    public TextMeshProUGUI tmp_nbrScore;

    // Start is called before the first frame update
    void Start()
    {
        n_nbrScore = 0;
        n_SauveAllie = GameObject.FindGameObjectsWithTag("Player").Length;
        tmp_nbrSauve.text = n_SauveAllie.ToString();
        tmp_nbrSauve.color = new Color(255, 255, 255, 0);
    }

    public void removeAllieSauve()
    {
        n_SauveAllie--;
        print("getting hit" + n_SauveAllie);
        tmp_nbrSauve.text = n_SauveAllie.ToString();
    }

    public void addScore()
    {
        n_nbrScore++;
        tmp_nbrScore.text = n_nbrScore.ToString() + " sur 8 allie sauvé";
        print(n_nbrScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (n_SauveAllie == 0)
        {
            go_menu.GetComponent<MenuPhase2>().startCoroutine(true);
        }
    }
}
