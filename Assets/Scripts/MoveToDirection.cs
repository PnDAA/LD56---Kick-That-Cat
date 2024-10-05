using UnityEngine;

public class MoveToDirection : MonoBehaviour
{
    [SerializeField] private float _speedMin;
    [SerializeField] private float _speedMax;
    [SerializeField] private Vector3 _direction;

    private float _speed;

    private void Awake()
    {
        _speed = Random.Range(_speedMin, _speedMax);
    }

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }
}
