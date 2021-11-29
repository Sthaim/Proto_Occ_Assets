using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public GameObject spawnobj;

    private Cube selectedCube;

    private GameObject[] listTag;

    private List<GameObject> listCube;

    private int nbrIteration;


    void Start()
    {
        listTag = GameObject.FindGameObjectsWithTag("Selected");
        if (listTag.Length > 0)
        {
            selectedCube = listTag[0].GetComponent<Cube>();
        }
        nbrIteration = 0; 
    }

    private void OnMouseDown()
    {
        listTag = GameObject.FindGameObjectsWithTag("Selected");
        if (listTag.Length > 0)
        {
            selectedCube = listTag[0].GetComponent<Cube>();
            listCube = selectedCube.GetComponent<Cube>().Waypoint;
            InvokeRepeating("Coroutine", 0F, 0.1F);
        }
        
    }

    public void callOnMouseDown()
    {
        OnMouseDown();
    }

    public void callOnMouseUp()
    {
        OnMouseUp();
    }

    private void OnMouseUp()
    {
        CancelInvoke("Coroutine");
        StopCoroutine(launchMove());
        if (selectedCube != null)
        {
            if (nbrIteration < 2)
            {
                selectedCube.removeWaypoint(listCube.Count-1);
            }
            else
            {
                FinTrait();
            }
            
        }
        nbrIteration = 0;

        
    }

    private void Coroutine()
    {
        StartCoroutine(launchMove());
    }

    IEnumerator launchMove()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(GetMouseWorldPos().origin, GetMouseWorldPos().direction, 100000f);
        Debug.Log(hits.Length);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null && hits[i].collider.tag == "Plane")
            {
                Debug.Log("Touche plane");
                GameObject go = Instantiate(spawnobj, hits[i].point, Quaternion.identity);
                selectedCube.addWaypoint(go);
                nbrIteration++;
                selectedCube.UpdateIteration(nbrIteration);
            }
        }
        
        yield return new WaitForSeconds(0.5f);
    }

    private void FinTrait()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(GetMouseWorldPos().origin, GetMouseWorldPos().direction, 100000f);
        
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[0].collider != null && hits[0].collider.tag == "Plane")
            {
                nbrIteration++;
                selectedCube.UpdateIteration(nbrIteration);

                

                GameObject go = Instantiate(spawnobj, hits[0].point, Quaternion.identity);
                selectedCube.addWaypoint(go);
            }
        }
    }


    private Ray GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePoint);
        return ray;
    }


    

}