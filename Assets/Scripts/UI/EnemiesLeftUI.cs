using TMPro;
using UnityEngine;

public class EnemiesLeftUI : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private TMP_Text _text;

    private int _enemiesLeftCount;

    private void Awake()
    {
        _enemiesLeftCount = _spawner.GetTotalToSpawnCount();
        _spawner.OnSpawned += OnSpawned;
    }

    private void OnSpawned(GameObject enemy)
    {
        enemy.GetComponent<Enemy>().OnDead += () => _enemiesLeftCount--;
    }

    private void Update()
    {
        _text.enabled = _enemiesLeftCount > 0;
        _text.text = $"{_enemiesLeftCount} Enemies left";
    }
}
