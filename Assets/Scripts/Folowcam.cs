using TMPro;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Seguimiento")]
    [SerializeField] Transform _target;
    [SerializeField] float _smoothSpeed = 5.0f;

    [Header("Vista de Águila")]
    [SerializeField] bool _eagleView = false;
    [SerializeField] float _eagleHeight = 15f;
    [SerializeField] float _eagleAngle = 90f; 
    [SerializeField] KeyCode _toggleKey = KeyCode.E;

    [Header("Auto Vista de Águila")]
    [SerializeField] bool _autoEagleView = true;
    [SerializeField] float _maxDistanceForNormalView = 20f;
    [SerializeField] float _hysteresis = 2f;

    [Header("Vista Normal")]
    [SerializeField] Vector3 _normalOffset = new Vector3(0, 5, -10);
    [SerializeField] float _normalHeight = 5f;
    [SerializeField] float _normalAngle = 10f;

    [Header("Rotación con Mouse")]
    [SerializeField] float _rotationSpeed = 3.0f;
    private float _mouseYaw = 0f;
    private bool _isRotating = false;

    private Vector3 _offset;
    private Vector3 _desirePosition;
    private Vector3 _smoothedPosition;
    private Vector3 _currentOffset;
    private float _transitionSpeed = 2f;

    void Start()
    {
        if (_offset == Vector3.zero)
        {
            _offset = _eagleView ? new Vector3(0, _eagleHeight, 0) : _normalOffset;
        }
        _currentOffset = _offset;
    }

    void Update()
    {
        // Toggle entre vista normal y vista de águila
        if (Input.GetKeyDown(_toggleKey))
        {
            ToggleEagleView();
        }
        if (Input.GetMouseButton(1))
        {
            _isRotating = true;

            Cursor.lockState = CursorLockMode.Locked;

            float mouseX = Input.GetAxis("Mouse X") * _rotationSpeed;
            _mouseYaw += mouseX;
        }
        else
        {
            _isRotating = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void LateUpdate()
    {
        if (_target == null) return;
        Vector3 targetOffset = _eagleView ?
            new Vector3(0, _eagleHeight, 0) :
            _normalOffset;

        _currentOffset = Vector3.Lerp(_currentOffset, targetOffset, _transitionSpeed * Time.deltaTime);

        Quaternion yawRotation = Quaternion.Euler(0, _mouseYaw, 0);
        Vector3 rotatedOffset = yawRotation * _currentOffset;

        _desirePosition = _target.position + rotatedOffset;

        _smoothedPosition = Vector3.Lerp(transform.position, _desirePosition, _smoothSpeed * Time.deltaTime);
        transform.position = _smoothedPosition;


        if (!_isRotating && _eagleView)
        {
            transform.rotation = Quaternion.Euler(_eagleAngle, transform.eulerAngles.y, 0);
        }
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(_target.position - transform.position);
            transform.rotation = Quaternion.Euler(targetRotation.eulerAngles.x, _mouseYaw, 0);
        }
    }



    public void ToggleEagleView()
    {
        _eagleView = !_eagleView;
        Debug.Log($"Vista de águila: {(_eagleView ? "Activada" : "Desactivada")}");
    }

    public void SetEagleView(bool enable)
    {
        _eagleView = enable;
    }

    public bool IsEagleViewActive()
    {
        return _eagleView;
    }
}
