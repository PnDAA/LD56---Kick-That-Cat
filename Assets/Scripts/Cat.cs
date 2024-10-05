using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _fallingSpeed = 1f;

    [SerializeField] private Rigidbody2D _rigidBody;
    public Rigidbody2D RigidBody => _rigidBody;

    [SerializeField] private GameObject _toEnableOnWalking;
    [SerializeField] private GameObject _toEnableOnFlying;

    private float _hitGroundTime = 0f;
    private int _hitGroundCount = 0;

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
        _toEnableOnWalking.SetActive(false);
        _toEnableOnFlying.SetActive(true);
        _state = State.Shooted;
        StopAllCoroutines(); // in case we collide the ground before being shooted
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        int layer = collision2D.gameObject.layer;
        bool isEnemy = layer == LayerMask.NameToLayer("Enemy");
        bool isGround = layer == LayerMask.NameToLayer("Default");
        if (_state == State.Falling)
        {
            if (isEnemy) // (can only kill if shooted + don't want to stop the enemy too long)
                GameObject.Destroy(gameObject);
            else if (isGround)
                this.StartCoroutineDoAfterXSec(0.25f, () => GameObject.Destroy(gameObject)); // permit to be shooted for a frame (will cancel the coroutine)
        }
        else if (_state == State.Shooted)
        {
            if (isEnemy)
            {
                //
                collision2D.gameObject.GetComponent<Enemy>().HitByCat();
                GameObject.Destroy(gameObject);
            }
            else if (isGround)
            {
                // kill if too many rebounce or if collide with ground for too long
                _hitGroundTime = Time.time;
                _hitGroundCount++;
                if (_hitGroundCount >= 3)
                    GameObject.Destroy(gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision2D)
    {
        int layer = collision2D.gameObject.layer;
        bool isGround = layer == LayerMask.NameToLayer("Default");
        if (isGround && Time.time - _hitGroundTime >= 1f)
            GameObject.Destroy(gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.layer == LayerMask.NameToLayer("House"))
        {
            _state = State.Falling;
        }
    }
}
