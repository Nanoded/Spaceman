using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _gunTransform;
    [SerializeField] private LineRenderer _laser;
    [SerializeField] private float _laserDistance;
    private ObjectPool _bulletPool;
    private PlayerActions _playerActions;
    private Sequence _rotationAnimation;
    private AudioSource _shootSound;

    private void Start()
    {
        _shootSound = GetComponent<AudioSource>();
        _laser.enabled = false;
        EventHandler.TimerIsEndEvent.AddListener(DisableAttackBehaviour);
        EventHandler.ReturnMainMenuEvent.AddListener(DisableAttackBehaviour);
        EventHandler.RestartLevelEvent.AddListener(() => _rotationAnimation.Kill());
        CheckBullet();
    }

    private void CheckBullet()
    {
        if (_bulletPrefab.TryGetComponent(out IPoolableObject poolableObject))
        {
            _bulletPool = new GameObject("ShipBulletPool", typeof(ObjectPool)).GetComponent<ObjectPool>();
            _bulletPool.InitPool(poolableObject);
        }
        else
        {
            Debug.LogError("Bullet prefab is not poolable object! And the ship refuses to fire! (AttackBehaviour is disable)");
            enabled = false;
        }
    }

    private void Awake()
    {
        _playerActions = new PlayerActions();
    }

    private void LeftClickAction(InputAction.CallbackContext obj)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        StartCoroutine(WaitForShoot(2));
    }

    private IEnumerator WaitForShoot(int seconds)
    {
        _rotationAnimation.Pause();
        _playerActions.Disable();
        Shoot();
        yield return new WaitForSeconds(seconds);
        _rotationAnimation.Play();
        _playerActions.Enable();
    }

    private void Update()
    {
        LaserAimer();
    }

    private void LaserAimer()
    {
        _laser.SetPosition(0, _gunTransform.position);
        if (Physics.Raycast(_gunTransform.position, _gunTransform.forward, out RaycastHit hit))
        {
            _laser.SetPosition(1, hit.point);
        }
        else
        {
            _laser.SetPosition(1, _gunTransform.forward * _laserDistance);
        }
    }
    private void Shoot()
    {
        _shootSound.Play();
        _bulletPool.GetOrCreateObject(transform).UseObject();
    }

    public void EnableAttackBehaviour(Sequence rotateAnimation)
    {
        _rotationAnimation = rotateAnimation; 
        _rotationAnimation.Play();
        _laser.enabled = true;
        _playerActions.MouseActions.LeftClick.performed += LeftClickAction;
        _playerActions.Enable();
    }

    public void DisableAttackBehaviour()
    {
        _playerActions.Disable();
        _rotationAnimation.Kill();
        _laser.enabled = false;
    }
}
