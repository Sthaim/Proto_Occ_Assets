using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public List<GameObject> Waypoint;
    public LineRenderer prefabLineRend;
    private GameObject[] listTag;
    public float Offset;
    private Vector3 offsetPos;
    private int nbrIte;
    private int dernierCount;
    private bool moving;
    private Vector3 dernierePos;
    private GameObject plane;

    private void Start()
    {
        prefabLineRend=Instantiate(prefabLineRend, new Vector3(0, 0, 0), Quaternion.identity);
        plane = GameObject.FindWithTag("Plane");
    }

    private void Update()
    {
        if (Waypoint.Count > 0 && dernierCount>0) {
            offsetPos = new Vector3(Waypoint[0].transform.position.x, Waypoint[0].transform.position.y + Offset, Waypoint[0].transform.position.z);
            dernierePos = gameObject.transform.position;

            var lookPos = offsetPos - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation=Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, offsetPos, Time.deltaTime * 50f);

            if (gameObject.transform.position== dernierePos)
            {
                moving = false;
            }
            
            if (Vector3.Distance(gameObject.transform.position, offsetPos)<10)
            {
                removeWaypoint(0);
            }
        }
        if (nbrIte == 2 && Waypoint.Count > 2 && moving == true)
        {
            Debug.Log("SupprDerniereLigne");
            int count = Waypoint.Count;
            for (int i = 0; i < count-2; i++)
            {
                removeWaypoint(0);
            }
        }
        if (nbrIte > 2 )
        {
            moving = true;
        }

    }

    public void UpdateIteration( int ite)
    {
        nbrIte = ite;
    }

    private void OnMouseDown()
    {
        plane.GetComponent<MoveObject>().callOnMouseDown();
        if (gameObject.CompareTag("Untagged"))
        {
            listTag = GameObject.FindGameObjectsWithTag("Selected");
            foreach (GameObject cube in listTag)
            {
                cube.tag = "Untagged";
                cube.GetComponent<Renderer>().material.color = Color.white;
                cube.GetComponent<Cube>().prefabLineRend.SetColors(Color.white, Color.white);
                foreach (Renderer variableName in cube.GetComponentsInChildren<Renderer>())
                {
                    variableName.material.color = Color.white;
                }
            }

            gameObject.tag = "Selected";
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
            gameObject.GetComponent<Cube>().prefabLineRend.SetColors(Color.black, Color.green);
            foreach (Renderer variableName in GetComponentsInChildren<Renderer>())
            {
                variableName.material.color = Color.blue;
            }
        }
    }

    private void OnMouseUp()
    {
        plane.GetComponent<MoveObject>().callOnMouseUp();
    }

    public void addWaypoint(GameObject gameObj)
    {
        dernierCount = Waypoint.Count;
        Waypoint.Add(gameObj);
        prefabLineRend.positionCount = Waypoint.Count;
        for(int i = 0; i < prefabLineRend.positionCount; i++)
        {
            prefabLineRend.SetPosition(i,Waypoint[i].transform.position);
        }
    }

    public void removeWaypoint(int index)
    {
       
        dernierCount = Waypoint.Count;
        Vector3[] newPositions = new Vector3[prefabLineRend.positionCount - 1];

        for (int i = 0; i < index; i++)
        {
            newPositions[i] = prefabLineRend.GetPosition(i);


        }
        for (int i = index; i < newPositions.Length; i++)
        {
            newPositions[i] = prefabLineRend.GetPosition(i + 1);

           
        }
        prefabLineRend.positionCount=newPositions.Length;
        prefabLineRend.SetPositions(newPositions);

        Waypoint[index].SetActive(false);
        Destroy(Waypoint[index]);
        Waypoint.RemoveAt(index);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
            int count = Waypoint.Count;
            for (int i = 0; i < count;i++)
            {
                removeWaypoint(0);
            }
        }
    }
}
