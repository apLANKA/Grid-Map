using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Selection : MonoBehaviour
{
    private GameObject selectedObject;
    [SerializeField] private PlacementSystem placementSystem;
    [SerializeField] private Grid grid;
    [SerializeField] private InputSystem inputSystem;
    [SerializeField] private ObjectDatabaseSO database;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (hit.collider.gameObject.CompareTag("Object") || hit.collider.gameObject.CompareTag("Building"))
                {
                    Select(hit.collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(2) && selectedObject != null)
        {
            Deselect();
        }

        
    }

    void Select(GameObject selectPrefab)
    {
        if (selectPrefab == selectedObject) return;
        if (selectedObject != null) Deselect();
        Outline outline = selectPrefab.GetComponent<Outline>();
        if (outline == null) selectPrefab.AddComponent<Outline>();
        else outline.enabled = true;
        selectedObject = selectPrefab;
    }

    void Deselect()
    {
        selectedObject.GetComponent<Outline>().enabled = false;
        selectedObject = null;
    }
    
    
    
    public void Delete()
    {
        GameObject objectToDestroy = selectedObject;
        Deselect();
        Destroy(objectToDestroy);
    }

   
}

