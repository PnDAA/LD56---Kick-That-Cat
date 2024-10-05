using UnityEngine;

public class EntityStopper : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject.Destroy(collider.gameObject);
    }
}
