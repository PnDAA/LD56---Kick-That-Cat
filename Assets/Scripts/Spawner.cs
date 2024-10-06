using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _rate = 1f;
    [SerializeField] private GameObject[] _prefabs;

    private float _time = 0f;

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time >= 1f/_rate)
        {
            _time = 0f;
            GameObject prefab = _prefabs.TakeOneRandom();
            GameObject.Instantiate(prefab, parent: transform, position: transform.position, rotation: transform.rotation);
        }
    }
}
