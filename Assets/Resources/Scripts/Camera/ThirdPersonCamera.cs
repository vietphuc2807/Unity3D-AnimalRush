using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private CameraSettings cameraSettings;
    private Transform cam;

    private float xRotation;
    private float yRotation;

    private float smoothX;
    private float smoothY;

    private float smoothXVelocity;
    private float smoothYVelocity;

    private float currentZoom;

    private bool followBoat = false;

    #region Start
    private void Start()
    {
        cam = GetComponentInChildren<Camera>().transform;

        currentZoom = cameraSettings.DefaultZoom;
        cam.position = new Vector3(0f, 0f, -currentZoom);
    }
    #endregion

    #region Update
    private void Update()
    {
        HandleCursor();

        if (!followBoat) HandleRotation();

        HandleZoom();
    }
    #endregion

    #region LateUpdate
    private void LateUpdate()
    {
        FollowTarget();
        
        if (followBoat) FollowRotation();
    }
    #endregion

    #region FollowTarget
    private void FollowTarget()
    {
        if (player == null) return;

        transform.position = player.position + Vector3.up * cameraSettings.FollowHeight;
    }
    #endregion

    #region FollowRotation
    private void FollowRotation()
    {
        Quaternion targetRotation = Quaternion.Euler(0, player.eulerAngles.y, 0);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            cameraSettings.RotationSmooth * Time.deltaTime
        );
    }
    #endregion

    #region HandleRotation
    private void HandleRotation()
    {
        if (!Input.GetMouseButton(1)) return;

        float mouseX = Input.GetAxis("Mouse X") * cameraSettings.MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSettings.MouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, cameraSettings.MinVerticalAngle, cameraSettings.MaxVerticalAngle);

        smoothX = Mathf.SmoothDamp(smoothX, xRotation, ref smoothXVelocity, cameraSettings.SmoothTime);
        smoothY = Mathf.SmoothDamp(smoothY, yRotation, ref smoothYVelocity, cameraSettings.SmoothTime);

        transform.rotation = Quaternion.Euler(smoothX, smoothY, 0f);
    }
    #endregion

    #region HandleZoom
    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            currentZoom -= scroll * cameraSettings.ZoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, cameraSettings.MinZoom, cameraSettings.MaxZoom);
        }

        cam.localPosition = new Vector3(0f, 0f, -currentZoom);
    }
    #endregion

    #region HandleCursor
    private void HandleCursor()
    {
        if(followBoat) return;

        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    #endregion

    #region SetBoatCamera
    public void SetBoatCamera(bool value)
    {
        followBoat = value;
    }
    #endregion
}