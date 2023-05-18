using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private InputSystem inputSystem;
    [SerializeField] private Grid grid;
    private GameObject previewObject;
    [SerializeField] private ObjectDatabaseSO database;
    private int selectedObjectIndex = -1;
    [SerializeField] private GameObject gridVisualization;
    [SerializeField] private PreviewSystem previewSystem;
    
    private void Start()
    {
        StopPlacement();
    }
    
    public void StartPlacement(int iD)
    {
        StopPlacement();
        selectedObjectIndex = database.objectDatas.FindIndex(data => data.ID == iD);
        if (selectedObjectIndex < 0)
        {
            return;
        }
        gridVisualization.SetActive(true);
        previewSystem.StartShowingPreview(database.objectDatas[selectedObjectIndex].Prefab, database.objectDatas[selectedObjectIndex].Size);
        inputSystem.OnClick += PlaceStructure;
        inputSystem.OnExit += StopPlacement;
            
    }

    private void PlaceStructure()
    {
        
            if (inputSystem.IsPointOverUI())
            {
                return;
            }
            Vector3 mousePosition = inputSystem.GetSelectedMapPosition();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);

           
            
            
            GameObject newObject = Instantiate(database.objectDatas[selectedObjectIndex].Prefab);
            newObject.transform.position = grid.CellToWorld(gridPosition);
            newObject.transform.rotation = previewSystem.previewPrefab.transform.rotation;

            StopPlacement();

    }
    

    private void StopPlacement()
    {
        selectedObjectIndex = -1;
        gridVisualization.SetActive(false);
        previewSystem.StopShowingPreview();
        inputSystem.OnClick -= PlaceStructure;
        inputSystem.OnExit -= StopPlacement;
    }

   
    
    public void Update()
    {
        if (selectedObjectIndex < 0)
            return;
        
        Vector3 mousePosition = inputSystem.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition));

        if (Input.GetMouseButtonDown(1))
            previewSystem.previewPrefab.transform.Rotate(0, 90, 0);
        

    }
    
}
