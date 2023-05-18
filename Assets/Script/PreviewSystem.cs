using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSystem : MonoBehaviour
{
    
    [SerializeField] private GameObject cellIndicator;
    [SerializeField] public GameObject previewPrefab;
    [SerializeField] private Material whiteMaterial;

    private void Start()
    {
        cellIndicator.SetActive(false);
    }
    

    public void StartShowingPreview(GameObject prefab, Vector2Int size)
    {
        PrepareIndicator(size);
        cellIndicator.SetActive(true);

        previewPrefab = Instantiate(prefab);
        PreparePreviewObject(previewPrefab);
    }

    private void PreparePreviewObject(GameObject previewPrefabs)
    {
        MeshRenderer meshRenderer = previewPrefabs.GetComponentInChildren<MeshRenderer>();
        meshRenderer.material = whiteMaterial;
        

    }

    public void StopShowingPreview()
    {
        cellIndicator.SetActive(false);
        Destroy(previewPrefab);
    }

    private void PrepareIndicator(Vector2Int size)
    {
        if (size.x > 0 || size.y > 0)
        {
            cellIndicator.transform.localScale = new Vector3(size.x, 1, size.y);
        }
    }

    public void UpdatePosition(Vector3 position)
    {
        MoveIndicator(position);
        
    }

    private void MoveIndicator(Vector3 position)
    {
        cellIndicator.transform.position = position;
        previewPrefab.transform.position = position;
    }
    
}
