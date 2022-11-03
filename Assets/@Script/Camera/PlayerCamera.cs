using UnityEngine;

public class PlayerCamera : BaseCamera
{
    [SerializeField] public float cameraSpeed;           // 카메라 속도
    [SerializeField] public float sensitivity;           // 카메라 민감도
    [SerializeField] public float clampAngle;            // 시야각
    [SerializeField] public float smoothness;            // 
    
    [SerializeField] public float minDistance;           // 최소 거리
    [SerializeField] public float maxDistance;           // 최대 거리

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
        mouseRotateX += -(Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime); // 카메라 X축 회전은 마우스 Y 좌표에 의해 결정됨
        mouseRotateY += Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;    // 카메라 Y축 회전은 마우스 X 좌표에 의해 결정됨

        mouseRotateX = Mathf.Clamp(mouseRotateX, -ClampAngle, ClampAngle);

        Quaternion mouseRotate = Quaternion.Euler(mouseRotateX, mouseRotateY, 0);
        transform.rotation = mouseRotate;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetTransform.position, CameraSpeed * Time.deltaTime);
        finalDirection = transform.TransformPoint(normalizedDirection * MaxDistance);

        RaycastHit hitObject;

        if (Physics.Linecast(transform.position, finalDirection, out hitObject))
        {
            finalDistance = Mathf.Clamp(hitObject.distance, MinDistance, MaxDistance);
        }

        else
        {
            finalDistance = MaxDistance;
        }

        ThisCamera.transform.localPosition = Vector3.Lerp(ThisCamera.transform.localPosition, normalizedDirection * finalDistance, Time.deltaTime * Smoothness);
    }

    #region Property
    public float CameraSpeed
    {
        get { return cameraSpeed; }
        set { cameraSpeed = value; }
    }
    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }
    public float ClampAngle
    {
        get { return clampAngle; }
        set { clampAngle = value; }
    }
    public float Smoothness
    {
        get { return smoothness; }
        set { smoothness = value; }
    }

    public float MinDistance
    {
        get { return minDistance; }
        set { minDistance = value; }
    }
    public float MaxDistance
    {
        get { return maxDistance; }
        set { maxDistance = value; }
    }
    #endregion
}
