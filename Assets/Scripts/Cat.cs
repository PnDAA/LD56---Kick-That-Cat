using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _fallingSpeed = 1f;

    [SerializeField] private Rigidbody2D _rigidBody;
    public Rigidbody2D RigidBody => _rigidBody;

    private enum State
    {
        Walking,
        Falling,
        Shooted
    }

    private State _state = State.Walking;

    private void FixedUpdate()
    {
        if (_state == State.Walking)
        {
            _rigidBody.transform.position += (Vector2.right * Time.fixedDeltaTime * _speed).ToVector3WithY0();
        }
        else if (_state == State.Falling)
        {
           _rigidBody.transform.position += (Vector2.right * Time.fixedDeltaTime * _fallingSpeed).ToVector3WithY0();
        }
    }

    public void SetShooted()
    {
        _state = State.Shooted;
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        int layer = collision2D.gameObject.layer;
        if (layer == LayerMask.NameToLayer("Default")) // ~ground
        {
            GameObject.Destroy(gameObject);
        }
        else if (_state == State.Shooted && layer == LayerMask.NameToLayer("Enemy")) // (can only kill if shooted)
        {
            collision2D.gameObject.GetComponent<Enemy>().HitByCat();
            GameObject.Destroy(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.layer == LayerMask.NameToLayer("House"))
        {
            _state = State.Falling;
        }
    }
}
