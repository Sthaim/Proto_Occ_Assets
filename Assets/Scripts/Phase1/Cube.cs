using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private GameObject go_gameController;
    public Animator a_halo;

    private void Start()
    {
        prefabLineRend=Instantiate(prefabLineRend, new Vector3(0, 0, 0), Quaternion.identity);
        plane = GameObject.FindWithTag("Plane");
        go_gameController = GameObject.FindGameObjectWithTag("GameController");
        
    }

    private void Update()
    {
        if (Waypoint.Count > 0 && dernierCount>0) {
            offsetPos = new Vector3(Waypoint[0].transform.position.x, Waypoint[0].transform.position.y + Offset, Waypoint[0].transform.position.z);
            dernierePos = gameObject.transform.position;

            var lookPos = offsetPos - transform.position;
            lookPos.y = 0;
            if (lookPos == Vector3.zero ) { }
            else
            {
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
            }
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, offsetPos, Time.deltaTime * 1f);

            if (gameObject.transform.position== dernierePos)
            {
                moving = false;
            }
            
            if (Vector3.Distance(gameObject.transform.position, offsetPos)<0.5)
            {
                removeWaypoint(0);
            }
        }
        if (nbrIte == 2 && Waypoint.Count > 2 && moving == true)
        {
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

    public void UpdateIteration( int _ite)
    {
        nbrIte = _ite;
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
                cube.GetComponent<Cube>().a_halo.SetTrigger("MovingStopped");
/*                cube.GetComponent<Renderer>().material.color = Color.white;*/
                cube.GetComponent<Cube>().prefabLineRend.SetColors(Color.white, Color.white);
                foreach (Renderer variableName in cube.GetComponentsInChildren<Renderer>())
                {
                    if (variableName.gameObject.CompareTag("Halo"))
                    {
                        variableName.material.color = Color.white;
                    }
                }
            }
            a_halo.SetTrigger("MovingStopped");
            gameObject.tag = "Selected";
/*            gameObject.GetComponent<Renderer>().material.color = Color.blue;*/
            gameObject.GetComponent<Cube>().prefabLineRend.SetColors(Color.black, Color.green);
            foreach (Renderer variableName in GetComponentsInChildren<Renderer>())
            {
                if (variableName.gameObject.CompareTag("Halo"))
                {
                    variableName.material.color = Color.blue;
                }
            }
        }
    }

    private void OnMouseUp()
    {
        plane.GetComponent<MoveObject>().callOnMouseUp();
    }

    public void addWaypoint(GameObject _gameObj)
    {
        dernierCount = Waypoint.Count;
        Waypoint.Add(_gameObj);
        prefabLineRend.positionCount = Waypoint.Count;
        for(int i = 0; i < prefabLineRend.positionCount; i++)
        {
            prefabLineRend.SetPosition(i,Waypoint[i].transform.position);
        }
    }

    public void removeWaypoint(int _index)
    {
       
        dernierCount = Waypoint.Count;
        Vector3[] newPositions = new Vector3[prefabLineRend.positionCount - 1];

        for (int i = 0; i < _index; i++)
        {
            newPositions[i] = prefabLineRend.GetPosition(i);


        }
        for (int i = _index; i < newPositions.Length; i++)
        {
            newPositions[i] = prefabLineRend.GetPosition(i + 1);

           
        }
        prefabLineRend.positionCount=newPositions.Length;
        prefabLineRend.SetPositions(newPositions);

        Waypoint[_index].SetActive(false);
        Destroy(Waypoint[_index]);
        Waypoint.RemoveAt(_index);
    }


    private void OnTriggerEnter(Collider _collision)
    {
        if (_collision.gameObject.CompareTag("Enemy"))
        {
            _collision.gameObject.SetActive(false);
            go_gameController.GetComponent<Phase1Manager>().removeEnemyAlive();
            go_gameController.GetComponent<Phase1Manager>().addScore();
        }
    }
}
