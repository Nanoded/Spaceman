using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float _maxCameraZoom;
    [SerializeField] private float _minCameraZoom;
    [SerializeField] private float _startZoom;
    private float _cameraPositionZ;
    private PlayerActions _playerActions;
    private Transform _mainCameraTransform;

    private void Start()
    {
        _playerActions = new PlayerActions();
        _playerActions.MouseActions.Zoom.performed += ZoomAction;
        _mainCameraTransform = Camera.main.transform;
        _cameraPositionZ = _mainCameraTransform.position.y;

        EventHandler.StartGameEvent.AddListener(() => _playerActions.Enable());
        EventHandler.ReturnMainMenuEvent.AddListener(() =>
        {
            _mainCameraTransform.DOMoveZ(_startZoom, 0.5f);
            _playerActions.Disable();
        });
    }

    private void OnDisable()
    {
        _playerActions.Disable();
    }

    private void ZoomAction(InputAction.CallbackContext obj)
    {
        _cameraPositionZ = Mathf.Clamp(_cameraPositionZ + obj.ReadValue<Vector2>().y, _minCameraZoom, _maxCameraZoom);
        _mainCameraTransform.position = new Vector3(_mainCameraTransform.position.x, _mainCameraTransform.position.y, _cameraPositionZ);
    }
}
