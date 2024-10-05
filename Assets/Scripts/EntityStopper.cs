using UnityEngine;

public class EntityStopper : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log($"Entity stopped killed: {collider.gameObject}");
        GameObject.Destroy(collider.gameObject);
    }
}
