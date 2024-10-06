using UnityEngine;

public class GlobalPrefabInstanciater : MonoBehaviour
{
    [SerializeField] private GameObject _smallHitPrefab;
    [SerializeField] private GameObject _hitPrefab;

    public static GlobalPrefabInstanciater Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void InstanciateHitPrefab(Vector3 position, float scale)
    {
        float angle = Random.Range(-20, 20);
        GameObject instanciated = GameObject.Instantiate(_hitPrefab, parent: transform, position: position, rotation: Quaternion.Euler(0, 0, angle));
        instanciated.transform.localScale *= scale;
        GameObject.Destroy(instanciated, t: 0.5f);
    }

    public void InstanciateSmallHitPrefab(Vector3 position, float scale)
    {
        GameObject instanciated = GameObject.Instantiate(_smallHitPrefab, parent: transform, position: position, rotation: Quaternion.identity);
        instanciated.transform.localScale *= scale;
        GameObject.Destroy(instanciated, t: 0.5f);
    }
}
