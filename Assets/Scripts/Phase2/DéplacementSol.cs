using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DéplacementSol : MonoBehaviour
{
    public GameObject spawnobj;

    private DeplacementFuite selectedFuite;

    private GameObject[] listTag;

    private List<GameObject> listCube;

    private int nbrIteration;


    void Start()
    {
        listTag = GameObject.FindGameObjectsWithTag("Selected");
        if (listTag.Length > 0)
        {
            selectedFuite = listTag[0].GetComponent<DeplacementFuite>();
        }
        nbrIteration = 0;
    }

    private void OnMouseDown()
    {
        listTag = GameObject.FindGameObjectsWithTag("Selected");
        if (listTag.Length > 0)
        {
            selectedFuite = listTag[0].GetComponent<DeplacementFuite>();
            listCube = selectedFuite.GetComponent<DeplacementFuite>().Waypoint;
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
        if (selectedFuite != null)
        {
            if (nbrIteration < 2)
            {
                selectedFuite.removeWaypoint(listCube.Count - 1);
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
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider != null && hits[i].collider.tag == "Plane")
            {
                GameObject go = Instantiate(spawnobj, hits[i].point, Quaternion.identity);
                selectedFuite.addWaypoint(go);
                nbrIteration++;
                selectedFuite.UpdateIteration(nbrIteration);
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
                selectedFuite.UpdateIteration(nbrIteration);



                GameObject go = Instantiate(spawnobj, hits[0].point, Quaternion.identity);
                selectedFuite.addWaypoint(go);
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

