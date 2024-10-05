using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HeroFoot : MonoBehaviour
{
    [SerializeField] private float _minStrength = 50f;
    [SerializeField] private float _maxStrength = 500f;
    [SerializeField] private float _radius = 1f;
    [SerializeField] private float _detectionRate = 10f;

    private bool _detectCollisions = false;
    private float _time = 0f;
    private float _strengthRatio;

    private float _scaledRadius => _radius * transform.lossyScale.x;

    private List<Cat> _shootedCats = new();

    public void ShootCatsInCollision()
    {
        Collider2D[] catsCollision = Physics2D.OverlapCircleAll(transform.position, _scaledRadius, LayerMask.GetMask("Cat", "FallingCat"));
        foreach (Collider2D catInCollision in catsCollision)
        {
            Cat cat = catInCollision.GetComponent<Cat>();

            // Don't want to shoot two times the same cat with the same "shoot".
            if (_shootedCats.Contains(cat))
                continue;

            _shootedCats.Add(cat);
            Vector2 direction = new Vector2(2, 3).normalized;
            cat.RigidBody.velocity = Vector2.zero;

            float strength = Mathf.Lerp(_minStrength, _maxStrength, _strengthRatio);
            cat.RigidBody.AddForce(direction * strength, ForceMode2D.Force);
            cat.SetShooted(_strengthRatio);
            Debug.Log($"Hit cat with strength: {strength}!");
        }
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
        _shootedCats?.Clear();
        _detectCollisions = true;
        _time = 0f;
        _strengthRatio = strengthRatio;
    }

    public void StopCatDetection()
    {
        _shootedCats?.Clear();
        _detectCollisions = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = (_detectCollisions ? Color.green : Color.red).WithAlpha(0.25f);
        Gizmos.DrawSphere(transform.position, _scaledRadius);
    }
}
