using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RondeEnemy : MonoBehaviour
{
    public List<GameObject> Ronde;
    public float tempsDeDépart;
    public NavMeshAgent agent;

    private readonly List<List<GameObject>> WaypointsListEnemy = new List<List<GameObject>>();


    private bool movingNow;
    private int indexTarget;
    private GameObject plane;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.name);
        plane = GameObject.FindWithTag("Plane");
        indexTarget = 0;
        movingNow = false;
    }

    public void StartMovement()
    {
        GameObject[] listTag;
        listTag = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in listTag)
        {
            if (enemy)
            {
                enemy.GetComponent<RondeEnemy>().go();
            }
        }
    }

    public void go()
    {
        StartCoroutine("StartingMovement");
    }

    IEnumerator StartingMovement()
    {
        yield return new WaitForSeconds(tempsDeDépart);
        newDestination();
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

    private void newDestination()
    {
        agent.SetDestination(Ronde[indexTarget].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (movingNow == true)
        {
            if (Ronde.Count > 0)
            {
                var lookPos = Ronde[indexTarget].transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);

                /*gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, WaypointsEnemy[indexTarget].transform.position, Time.deltaTime * 1f);*/

                if (Vector3.Distance(gameObject.transform.position, Ronde[indexTarget].transform.position) < 0.5)
                {
                    if (indexTarget < Ronde.Count - 1)
                    {
                        indexTarget++;
                        newDestination();
                    }
                    else{
                        indexTarget = 0;
                        newDestination();
                    }
                }
            }


        }
    }
}
