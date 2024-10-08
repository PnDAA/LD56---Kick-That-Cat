using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private float _speed = 4f;
    [SerializeField] private int _health = 1;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip[] _getHitSounds;
    [SerializeField] private float _getHitSoundPitch = 1f;

    private int _currentHealth;
    public event Action OnHealthRemoved;
    public event Action OnDead;

    private void Awake()
    {
        _currentHealth = _health;
        _animator.SetFloat("walk_speed", _speed);
    }

    private void FixedUpdate()
    {
        _rigidBody.transform.position += (Vector2.left * _speed * Time.fixedDeltaTime).ToVector3WithY0();
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.layer == LayerMask.NameToLayer("GameOverDetector"))
        {
            Debug.Log("DEFEAT");
            Game.Instance.DoGameOver();
        }
    }

    public void HitByCat()
    {
        Debug.Log("Got hit !");
        RemoveHealth();
        Game.Instance.SoundPlayer.Play(_getHitSounds.TakeOneRandom(), transform.position, pitch: _getHitSoundPitch);
    }

    private void RemoveHealth()
    {
        _currentHealth--;
        OnHealthRemoved?.Invoke();
        if (_currentHealth <= 0)
        {
            GameObject.Destroy(gameObject);
            OnDead?.Invoke();
        }
    }

    public float GetHealthRatio()
    {
        return ((float) _currentHealth) / _health;
    }
}
