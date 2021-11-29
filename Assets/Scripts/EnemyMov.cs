using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMov : MonoBehaviour
{
    public List<GameObject> WaypointsEnemy1;
    public List<GameObject> WaypointsEnemy2;
    public List<GameObject> WaypointsEnemy3;
    public float tempsDeDépart;

    private List<GameObject> WaypointsEnemy;

    private readonly List<List<GameObject>> WaypointsListEnemy = new List<List<GameObject>>();
    

    private bool movingNow;
    private int indexTarget;
    private GameObject plane;

    // Start is called before the first frame update
    void Start()
    {
        plane = GameObject.FindWithTag("Plane");
        Debug.Log(WaypointsListEnemy.Count);
        Debug.Log("enemie start");
        if (WaypointsEnemy1.Count > 0 && WaypointsEnemy2.Count > 0 && WaypointsEnemy3.Count > 0)
        {
            WaypointsListEnemy.Add(WaypointsEnemy1);
            WaypointsListEnemy.Add(WaypointsEnemy2);
            WaypointsListEnemy.Add(WaypointsEnemy3);
            Debug.Log(Random.Range(0, 3));
            WaypointsEnemy = WaypointsListEnemy[Random.Range(0, 3)];
        }
        

        indexTarget = 0;
        movingNow = false;
    }

    public void StartMovement()
    {
        StartCoroutine("StartingMovement");
    }

    IEnumerator StartingMovement()
    {
        yield return new WaitForSeconds(tempsDeDépart);
        movingNow = true;
    }

    private void OnMouseDown()
    {
        plane.GetComponent<MoveObject>().callOnMouseDown();
    }

    private void OnMouseUp()
    {
        plane.GetComponent<MoveObject>().callOnMouseUp();
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
                    if (indexTarget < WaypointsEnemy.Count-1)
                    {
                        indexTarget++;
                    }
                }
            }

           
        }
    }
}
