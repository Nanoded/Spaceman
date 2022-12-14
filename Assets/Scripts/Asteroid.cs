using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Asteroid : MonoBehaviour
{
    [SerializeField] private float2 _moveSpeedRange;
    [SerializeField] private float2 _rotationSpeedRangeX;
    [SerializeField] private float2 _rotationSpeedRangeY;
    [SerializeField] private float2 _rotationSpeedRangeZ;
    private float _currentMoveSpeed;
    private Vector3 _rotationVector;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        CalculateNewMoveSettings(transform.position);
    }

    private void Update()
    {
        Vector3 newRotation = transform.rotation.eulerAngles + (_rotationVector * Time.deltaTime);
        transform.rotation = Quaternion.Euler(newRotation);
    }

    private void CalculateRotation()
    {
        float rotationSpeedX = Random.Range(_rotationSpeedRangeX.x, _rotationSpeedRangeX.y);
        float rotationSpeedY = Random.Range(_rotationSpeedRangeY.x, _rotationSpeedRangeY.y);
        float rotationSpeedZ = Random.Range(_rotationSpeedRangeZ.x, _rotationSpeedRangeZ.y);
        _rotationVector = new Vector3(rotationSpeedX, rotationSpeedY, rotationSpeedZ);
    }

    private void CalculateMovement()
    {
        _currentMoveSpeed = Random.Range(_moveSpeedRange.x, _moveSpeedRange.y);
        _rigidbody.velocity = Vector3.right * _currentMoveSpeed;
    }

    public void CalculateNewMoveSettings(Vector3 startMovePos)
    {
        transform.position = startMovePos;
        CalculateRotation();
        CalculateMovement();
    }
}
