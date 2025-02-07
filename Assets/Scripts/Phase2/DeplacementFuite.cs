using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeplacementFuite : MonoBehaviour
{
    public List<GameObject> Waypoint;
    public LineRenderer prefabLineRender;
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
        prefabLineRender = Instantiate(prefabLineRender, new Vector3(0, 0, 0), Quaternion.identity);
        plane = GameObject.FindWithTag("Plane");
    }

    private void Update()
    {
        if (Waypoint.Count > 0 && dernierCount > 0)
        {
            offsetPos = new Vector3(Waypoint[0].transform.position.x, Waypoint[0].transform.position.y + Offset, Waypoint[0].transform.position.z);
            dernierePos = gameObject.transform.position;

            var lookPos = offsetPos - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, offsetPos, Time.deltaTime * 1f);

            if (gameObject.transform.position == dernierePos)
            {
                moving = false;
            }

            if (Vector3.Distance(gameObject.transform.position, offsetPos) < 0.5)
            {
                removeWaypoint(0);
            }
        }
        if (nbrIte == 2 && Waypoint.Count > 2 && moving == true)
        {
            int count = Waypoint.Count;
            for (int i = 0; i < count - 2; i++)
            {
                removeWaypoint(0);
            }
        }
        if (nbrIte > 2)
        {
            moving = true;
        }

    }

    public void UpdateIteration(int ite)
    {
        nbrIte = ite;
    }

    private void OnMouseDown()
    {
        plane.GetComponent<DéplacementSol>().callOnMouseDown();
        if (gameObject.CompareTag("Untagged"))
        {
            listTag = GameObject.FindGameObjectsWithTag("Selected");
            foreach (GameObject cube in listTag)
            {
                cube.tag = "Untagged";
                cube.GetComponent<Renderer>().material.color = Color.white;
                cube.GetComponent<DeplacementFuite>().prefabLineRender.SetColors(Color.white, Color.white);
                foreach (Renderer variableName in cube.GetComponentsInChildren<Renderer>())
                {
                    variableName.material.color = Color.white;
                }
            }

            gameObject.tag = "Selected";
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            gameObject.GetComponent<DeplacementFuite>().prefabLineRender.SetColors(Color.black, Color.green);
            foreach (Renderer variableName in GetComponentsInChildren<Renderer>())
            {
                variableName.material.color = Color.red;
            }
        }
    }

    private void OnMouseUp()
    {
        plane.GetComponent<DéplacementSol>().callOnMouseUp();
    }

    public void addWaypoint(GameObject gameObj)
    {
        dernierCount = Waypoint.Count;
        Waypoint.Add(gameObj);
        prefabLineRender.positionCount = Waypoint.Count;
        for (int i = 0; i < prefabLineRender.positionCount; i++)
        {
            prefabLineRender.SetPosition(i, Waypoint[i].transform.position);
        }
    }

    public void removeWaypoint(int index)
    {

        dernierCount = Waypoint.Count;
        Vector3[] newPositions = new Vector3[prefabLineRender.positionCount - 1];

        for (int i = 0; i < index; i++)
        {
            newPositions[i] = prefabLineRender.GetPosition(i);
        }
        for (int i = index; i < newPositions.Length; i++)
        {
            newPositions[i] = prefabLineRender.GetPosition(i + 1);
        }
        prefabLineRender.positionCount = newPositions.Length;
        prefabLineRender.SetPositions(newPositions);

        Waypoint[index].SetActive(false);
        Destroy(Waypoint[index]);
        Waypoint.RemoveAt(index);
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }
}

