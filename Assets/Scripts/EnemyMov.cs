using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMov : MonoBehaviour
{
    public List<GameObject> WaypointsEnemy1;
    public List<GameObject> WaypointsEnemy2;
    public List<GameObject> WaypointsEnemy3;

    private List<GameObject> WaypointsEnemy;
    public List<List<GameObject>> WaypointsListEnemy;
    

    private bool movingNow;
    private int indexTarget;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("enemie start");
        WaypointsListEnemy.Add(WaypointsEnemy1);
        WaypointsListEnemy.Add(WaypointsEnemy2);
        WaypointsListEnemy.Add(WaypointsEnemy3);

        WaypointsEnemy = WaypointsListEnemy[Random.Range(1, 3)];
        indexTarget = 0;
        movingNow = false;
        StartCoroutine("StartingMovement");
    }

    IEnumerator StartingMovement()
    {
        yield return new WaitForSeconds(2f);
        movingNow = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingNow == true)
        {
            if(WaypointsEnemy.Count > 0)
            {
                var lookPos = WaypointsEnemy[indexTarget].transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);

                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, WaypointsEnemy[indexTarget].transform.position, Time.deltaTime * 50f);

                if (Vector3.Distance(gameObject.transform.position, WaypointsEnemy[indexTarget].transform.position) < 10)
                {
                    if (indexTarget < WaypointsEnemy.Count)
                    {
                        indexTarget++;
                    }
                }
            }

           
        }
    }
}
