using UnityEngine;

public class PlayerCamera : BaseCamera
{
    [SerializeField] private float cameraSpeed;           // 카메라 속도
    [SerializeField] private float sensitivity;           // 카메라 민감도
    [SerializeField] private float clampAngle;            // 시야각
    [SerializeField] private float smoothness;            // 
    
    [SerializeField] private float minDistance;           // 최소 거리
    [SerializeField] private float maxDistance;           // 최대 거리

    private float mouseRotateX;         // 마우스 이동에 따른 X축 회전
    private float mouseRotateY;         // 마우스 이동에 따른 Y축 회전

    private Vector3 normalizedDirection;
    private Vector3 finalDirection;
    private float finalDistance;

    protected override void Awake()
    {
        base.Awake();
        Managers.GameManager.PlayerCamera = this;
    }

    private void Start()
    {
        mouseRotateX = transform.localRotation.eulerAngles.x;
        mouseRotateY = transform.localRotation.eulerAngles.y;

        normalizedDirection = ThisCamera.transform.localPosition.normalized;
        finalDistance = ThisCamera.transform.localPosition.magnitude;
    }

    private void Update()
    {
        mouseRotateX += -(Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime); // 카메라 X축 회전은 마우스 Y 좌표에 의해 결정됨
        mouseRotateY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;    // 카메라 Y축 회전은 마우스 X 좌표에 의해 결정됨

        mouseRotateX = Mathf.Clamp(mouseRotateX, -clampAngle, clampAngle);

        Quaternion mouseRotate = Quaternion.Euler(mouseRotateX, mouseRotateY, 0);
        transform.rotation = mouseRotate;
    }

    private void LateUpdate()
    {
        if (TargetTransform == null)
            return;

        transform.position = Vector3.MoveTowards(transform.position, TargetTransform.position + TargetOffset, cameraSpeed * Time.deltaTime);
        finalDirection = transform.TransformPoint(normalizedDirection * maxDistance);

        if (Physics.Linecast(transform.position, finalDirection, out RaycastHit hitObject))
        {
            finalDistance = Mathf.Clamp(hitObject.distance, minDistance, maxDistance);
        }

        else
        {
            finalDistance = maxDistance;
        }

        ThisCamera.transform.localPosition = Vector3.Lerp(ThisCamera.transform.localPosition, normalizedDirection * finalDistance, Time.deltaTime * smoothness);
    }

    private void OnDestroy()
    {
        Managers.GameManager.PlayerCamera = null;
    }

    #region Property
    public float CameraSpeed { get { return cameraSpeed; } }
    public float Sensitivity { get { return sensitivity; } set { sensitivity = value; } }
    public float ClampAngle { get { return clampAngle; } }
    public float Smoothness { get { return smoothness; } }
    public float MinDistance { get { return minDistance; } }
    public float MaxDistance { get { return maxDistance; } }
    #endregion
}
