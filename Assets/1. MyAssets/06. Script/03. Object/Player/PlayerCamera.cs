using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    // Private Variable
    [SerializeField] public GameObject targetObject;     // Ÿ��(�÷��̾�) ������Ʈ
    [SerializeField] public Transform playerCameraTransform;      // ����(�÷��̾�) ī�޶� Ʈ������
    
    [SerializeField] public float cameraSpeed;           // ī�޶� �ӵ�
    [SerializeField] public float sensitivity;           // ī�޶� �ΰ���
    [SerializeField] public float clampAngle;            // �þ߰�
    [SerializeField] public float smoothness;            // 
    
    [SerializeField] public float minDistance;           // �ּ� �Ÿ�
    [SerializeField] public float maxDistance;           // �ִ� �Ÿ�

    private float mouseRotateX;         // ���콺 �̵��� ���� X�� ȸ��
    private float mouseRotateY;         // ���콺 �̵��� ���� Y�� ȸ��

    private Vector3 normalizedDirection;
    private Vector3 finalDirection;
    private float finalDistance;

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        mouseRotateX = transform.localRotation.eulerAngles.x;
        mouseRotateY = transform.localRotation.eulerAngles.y;

        normalizedDirection = PlayerCameraTransform.localPosition.normalized;
        finalDistance = PlayerCameraTransform.localPosition.magnitude;
    }

    private void Update()
    {
        /*
        if (!GameManager.Instance.Player.IsInteract)
        {
            mouseRotateX += -(Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime); // ī�޶� X�� ȸ���� ���콺 Y ��ǥ�� ���� ������
            mouseRotateY += Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;    // ī�޶� Y�� ȸ���� ���콺 X ��ǥ�� ���� ������

            mouseRotateX = Mathf.Clamp(mouseRotateX, -ClampAngle, ClampAngle);

            Quaternion mouseRotate = Quaternion.Euler(mouseRotateX, mouseRotateY, 0);
            transform.rotation = mouseRotate;
        }
        */
        mouseRotateX += -(Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime); // ī�޶� X�� ȸ���� ���콺 Y ��ǥ�� ���� ������
        mouseRotateY += Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;    // ī�޶� Y�� ȸ���� ���콺 X ��ǥ�� ���� ������

        mouseRotateX = Mathf.Clamp(mouseRotateX, -ClampAngle, ClampAngle);

        Quaternion mouseRotate = Quaternion.Euler(mouseRotateX, mouseRotateY, 0);
        transform.rotation = mouseRotate;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetObject.transform.position, CameraSpeed * Time.deltaTime);
        finalDirection = transform.TransformPoint(normalizedDirection * MaxDistance);

        RaycastHit hitObject;

        if(Physics.Linecast(transform.position, finalDirection, out hitObject))
        {
            finalDistance = Mathf.Clamp(hitObject.distance, MinDistance, MaxDistance);
        }

        else
        {
            finalDistance = MaxDistance;
        }

        PlayerCameraTransform.localPosition = Vector3.Lerp(PlayerCameraTransform.localPosition, normalizedDirection * finalDistance, Time.deltaTime * Smoothness);
    }

    #region Property
    public GameObject TargetObject
    {
        get { return targetObject; }
        set { targetObject = value; }
    }
    public Transform PlayerCameraTransform
    {
        get { return playerCameraTransform; }
        set { playerCameraTransform = value; }
    }

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
