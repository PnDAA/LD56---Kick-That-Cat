using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private float _speed = 4f;
    [SerializeField] private int _health = 1;

    private int _currentHealth;
    public event Action OnHealthRemoved;

    private void Awake()
    {
        _currentHealth = _health;
    }

    private void FixedUpdate()
    {
        _rigidBody.transform.position += (Vector2.left * _speed * Time.fixedDeltaTime).ToVector3WithY0();
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.layer == LayerMask.NameToLayer("House"))
        {
            Debug.Log("DEFEAT");
            GameObject.Destroy(gameObject);
            Game.Instance.DoGameOver();
        }
    }

    public void HitByCat()
    {
        Debug.Log("Got hit !");
        RemoveHealth();
    }

    private void RemoveHealth()
    {
        _currentHealth--;
        OnHealthRemoved?.Invoke();
        if (_currentHealth <= 0)
            GameObject.Destroy(gameObject);
    }

    public float GetHealthRatio()
    {
        return ((float) _currentHealth) / _health;
    }
}
