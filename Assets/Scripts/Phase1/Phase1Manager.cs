using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Phase1Manager : MonoBehaviour
{
    private int n_enemyAlive;
    public TextMeshProUGUI tmp_nbrEnemy;
    private int n_nbrScore;
    public TextMeshProUGUI tmp_nbrScore;

    // Start is called before the first frame update
    void Start()
    {
        n_nbrScore = 0; 
        n_enemyAlive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        tmp_nbrEnemy.text = n_enemyAlive.ToString();
        tmp_nbrEnemy.color = new Color(255, 255, 255, 0);

    }

    public void removeEnemyAlive()
    {
        n_enemyAlive--;
        print("getting hit" + n_enemyAlive);
        tmp_nbrEnemy.text = n_enemyAlive.ToString();
    }

    public void addScore()
    {
        n_nbrScore++;
        tmp_nbrScore.text = n_nbrScore.ToString() + " sur 8 enemies tués";
        print(n_nbrScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (n_enemyAlive == 0)
        {
            print("fin");
            GetComponent<Menu>().startCoroutine(true);
        }
    }

    public void changeScene()
    {
        SceneManager.LoadScene("Phase2");
    }
}
