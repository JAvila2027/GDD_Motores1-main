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
    [SerializeField] float _eagleAngle = 90f; // Ángulo hacia abajo (90 = completamente hacia abajo)
    [SerializeField] KeyCode _toggleKey = KeyCode.E;

    [Header("Auto Vista de Águila")]
    [SerializeField] bool _autoEagleView = true;
    [SerializeField] float _maxDistanceForNormalView = 20f; // Distancia máxima para vista normal
    [SerializeField] float _hysteresis = 2f; // Para evitar cambios constantes

    [Header("Vista Normal")]
    [SerializeField] Vector3 _normalOffset = new Vector3(0, 5, -10);
    [SerializeField] float _normalHeight = 5f;
    [SerializeField] float _normalAngle = 10f;

    private Vector3 _offset;
    private Vector3 _desirePosition;
    private Vector3 _smoothedPosition;
    private Vector3 _currentOffset;
    private float _transitionSpeed = 2f;

    void Start()
    {
        // Si no tienes offset configurado, usa la posición inicial
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
    }

    void LateUpdate()
    {
        if (_target == null) return;

        // Determinar el offset objetivo
        Vector3 targetOffset = _eagleView ?
            new Vector3(0, _eagleHeight, 0) :
            _normalOffset;

        // Transición suave entre offsets
        _currentOffset = Vector3.Lerp(_currentOffset, targetOffset, _transitionSpeed * Time.deltaTime);

        // Posición deseada con offset
        _desirePosition = _target.position + _currentOffset;

        // Interpolación suave
        _smoothedPosition = Vector3.Lerp(transform.position, _desirePosition, _smoothSpeed * Time.deltaTime);
        transform.position = _smoothedPosition;

        // Rotación de la cámara
        UpdateCameraRotation();
    }

    void UpdateCameraRotation()
    {
        if (_target == null) return;

        if (_eagleView)
        {
            // En vista de águila, mirar directamente hacia abajo
            Vector3 direction = (_target.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _smoothSpeed * Time.deltaTime);
        }
        else
        {
            // Vista normal - mirar hacia el target con un ángulo
            Vector3 direction = (_target.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _smoothSpeed * Time.deltaTime);
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
