using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float movementSpeed;
    public float zoomSpeed;
    public float minZoom = 1f;
    public float maxZoom = 10;
    
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        CameraZoom();
        CameraMove();
    }

    void CameraZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - scroll * zoomSpeed, minZoom, maxZoom);
    }

    void CameraMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        transform.position += movementSpeed * Time.deltaTime * direction;
    }
    
}
