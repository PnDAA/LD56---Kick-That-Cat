using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private float _speed = 4f;

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
        }
    }

    public void HitByCat()
    {
        GameObject.Destroy(gameObject);
    }
}
