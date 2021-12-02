using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
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
    private GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        prefabLineRend = Instantiate(prefabLineRend, new Vector3(0, 0, 0), Quaternion.identity);
        plane = GameObject.FindWithTag("Plane");
        UI = GameObject.FindWithTag("UI");
    }

    private void OnMouseDown()
    {
        plane.GetComponent<PlaneMov>().callOnMouseDown();
        if (gameObject.CompareTag("Untagged") && UI.GetComponent<Menu1>().finishedAppear==true)
        {

            gameObject.tag = "Selected";
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
            gameObject.GetComponent<Movement>().prefabLineRend.SetColors(Color.black, Color.green);
            foreach (Renderer variableName in GetComponentsInChildren<Renderer>())
            {
                variableName.material.color = Color.blue;
                variableName.material.color = Color.blue;
            }
            UI.GetComponent<Menu1>().startCoroutine(UI.GetComponent<Menu1>().firstTextAppear, false, 1);
            UI.GetComponent<Menu1>().startCoroutine(UI.GetComponent<Menu1>().secondTextAppear, false, 1);
            UI.GetComponent<Menu1>().startCoroutine(UI.GetComponent<Menu1>().thirdTextAppear, true, 1);
        }
    }

    private void OnMouseUp()
    {
        plane.GetComponent<PlaneMov>().callOnMouseUp();
    }

    // Update is called once per frame
    void Update()
    {
        if (Waypoint.Count > 0 && dernierCount > 0)
        {
            offsetPos = new Vector3(Waypoint[0].transform.position.x, Waypoint[0].transform.position.y + Offset, Waypoint[0].transform.position.z);
            dernierePos = gameObject.transform.position;

            var lookPos = offsetPos - transform.position;
            lookPos.y = 0;
            if (lookPos == Vector3.zero) { }
            else
            {
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
            }
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
            UI.GetComponent<Menu1>().startCoroutine(UI.GetComponent<Menu1>().thirdTextAppear, false, 1);
        }
    }

    public void addWaypoint(GameObject gameObj)
    {
        dernierCount = Waypoint.Count;
        Waypoint.Add(gameObj);
        prefabLineRend.positionCount = Waypoint.Count;
        for (int i = 0; i < prefabLineRend.positionCount; i++)
        {
            prefabLineRend.SetPosition(i, Waypoint[i].transform.position);
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
        prefabLineRend.positionCount = newPositions.Length;
        prefabLineRend.SetPositions(newPositions);

        Waypoint[index].SetActive(false);
        Destroy(Waypoint[index]);
        Waypoint.RemoveAt(index);
    }

    public void UpdateIteration(int ite)
    {
        nbrIte = ite;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
