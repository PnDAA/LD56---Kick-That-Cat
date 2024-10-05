using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private float _speed = 4f;
    [SerializeField] private float _fallingSpeed = 1f;

    [SerializeField] private Rigidbody2D _rigidBody;
    public Rigidbody2D RigidBody => _rigidBody;

    [SerializeField] private GameObject _toEnableOnWalking;
    [SerializeField] private GameObject _toEnableOnFlying;
    [SerializeField] private TrailRenderer _trail;

    private float _strengthRatioShooted;

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
        else if (_state == State.Shooted)
        {
            _toEnableOnFlying.transform.Rotate(new Vector3(0, 0, 1), -Time.deltaTime * _strengthRatioShooted * 720f);
        }
    }

    public void SetShooted(float strengthRatio)
    {
        _toEnableOnWalking.SetActive(false);
        _toEnableOnFlying.SetActive(true);
        _state = State.Shooted;
        _strengthRatioShooted = strengthRatio;
        gameObject.layer = LayerMask.NameToLayer("Cat"); // allow collision with enemies.
        StopAllCoroutines(); // in case we collide the ground before being shooted
        _trail.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        int layer = collision2D.gameObject.layer;
        bool isEnemy = layer == LayerMask.NameToLayer("Enemy");
        bool isGround = layer == LayerMask.NameToLayer("Default");
        if (_state == State.Falling)
        {
            // (can't be enemy cause FallingCat don't collide with ground)
            if (isGround)
            {
                Debug.Log("Collide with ground while falling");
                this.StartCoroutineDoAfterXSec(0.1f, () => GameObject.Destroy(gameObject)); // permit to be shooted for a frame (will cancel the coroutine)
            }
        }
        else if (_state == State.Shooted)
        {
            if (isEnemy)
            {
                //
                Debug.Log("Collide with enemy");
                collision2D.gameObject.GetComponent<Enemy>().HitByCat();
                GameObject.Destroy(gameObject);
            }
            else if (isGround)
            {
                Debug.Log("Collide with ground");
                GameObject.Destroy(gameObject);
            }
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
