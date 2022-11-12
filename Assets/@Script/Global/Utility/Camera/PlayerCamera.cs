using UnityEngine;

public class PlayerCamera : BaseCamera
{
    [SerializeField] private float cameraSpeed;           // ī�޶� �ӵ�
    [SerializeField] private float sensitivity;           // ī�޶� �ΰ���
    [SerializeField] private float clampAngle;            // �þ߰�
    [SerializeField] private float smoothness;            // 
    
    [SerializeField] private float minDistance;           // �ּ� �Ÿ�
    [SerializeField] private float maxDistance;           // �ִ� �Ÿ�

    private float mouseRotateX;         // ���콺 �̵��� ���� X�� ȸ��
    private float mouseRotateY;         // ���콺 �̵��� ���� Y�� ȸ��

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
        mouseRotateX += -(Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime); // ī�޶� X�� ȸ���� ���콺 Y ��ǥ�� ���� ������
        mouseRotateY += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;    // ī�޶� Y�� ȸ���� ���콺 X ��ǥ�� ���� ������

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
