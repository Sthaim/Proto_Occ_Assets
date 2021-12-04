using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Manager : MonoBehaviour
{
    private int n_enemyAlive;
    public GameObject go_menu;

    // Start is called before the first frame update
    void Start()
    {
        n_enemyAlive = GameObject.FindGameObjectsWithTag("Player").Length;
    }

    public void removeEnemyAlive()
    {
        n_enemyAlive--;
        print("getting hit" + n_enemyAlive);
    }

    // Update is called once per frame
    void Update()
    {
        if (n_enemyAlive == 0)
        {
            go_menu.GetComponent<MenuPhase2>().startCoroutine(true);
        }
    }
}
