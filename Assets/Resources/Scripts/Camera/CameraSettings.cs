using UnityEngine;

[CreateAssetMenu(fileName = "CameraSettings", menuName = "GameData/CameraSettings")]
public class CameraSettings : ScriptableObject
{
    #region Follow Settings

    [Header("Follow")]
    [SerializeField]private float followHeight = 1.5f;

    #endregion


    #region Rotation Settings

    [Header("Rotation")]
    [SerializeField] private float mouseSensitivity = 200f;
    [SerializeField] private float smoothTime = 0.08f;
    [SerializeField] private float minVerticalAngle = -40f;
    [SerializeField] private float maxVerticalAngle = 70f;
    [SerializeField] private float rotationSmooth = 5f;


    #endregion

    #region Zoom Settings

    [Header("Zoom")]
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 8f;
    [Header("Default Zoom")]
    [SerializeField] private float defaultZoom = 13.6f;


    #endregion

    public float FollowHeight => followHeight;

    public float MouseSensitivity => mouseSensitivity;

    public float SmoothTime => smoothTime;

    public float MinVerticalAngle => minVerticalAngle;

    public float MaxVerticalAngle => maxVerticalAngle;

    public float ZoomSpeed => zoomSpeed;

    public float MinZoom => minZoom;

    public float MaxZoom => maxZoom;

    public float DefaultZoom => defaultZoom;
    public float RotationSmooth => rotationSmooth;
}
