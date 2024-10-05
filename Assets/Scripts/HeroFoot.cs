using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HeroFoot : MonoBehaviour
{
    [SerializeField] private float _strengthMutliplier = 1f;
    [SerializeField] private float _radius = 1f;
    [SerializeField] private float _detectionRate = 10f;

    private bool _detectCollisions = false;
    private float _time = 0f;
    private float _strength;

    public void ShootCatsInCollision()
    {
        Collider2D[] catsCollision = Physics2D.OverlapCircleAll(transform.position, _radius, LayerMask.GetMask("Cat"));
        foreach (Collider2D catInCollision in catsCollision)
        {
            Cat cat = catInCollision.GetComponent<Cat>();
            Vector2 direction = new Vector2(1, 1);
            cat.RigidBody.AddForce(direction.normalized * _strength, ForceMode2D.Force);
            cat.SetShooted();
            Debug.Log($"Hit cat with strength: {_strength}!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = (_detectCollisions ? Color.green : Color.red).WithAlpha(0.25f);
        Gizmos.DrawSphere(transform.position, _radius);
    }

    private void Update()
    {
        if (_detectCollisions)
        {
            _time += Time.deltaTime;
            if (_time >= 1f / _detectionRate)
            {
                ShootCatsInCollision();
                _time = 0f;
            }
        }
    }

    public void StartCatDetection(float strengthRatio)
    {
        _detectCollisions = true;
        _time = 0f;
        _strength = strengthRatio * _strengthMutliplier;
    }

    public void StopCatDetection()
    {
        _detectCollisions = false;
    }
}
