using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    private class PrefabWithTime
    {
        public GameObject Prefab;
        public float Time;
    }

    [SerializeField] private PrefabWithTime[] _prefabs;
    public bool IsFinished => _currentIndex >= _prefabs.Length;

    private float _time;
    private int _currentIndex = -1;
    public int CurrentIndex => _currentIndex;

    private List<GameObject> _spawned;
    public IEnumerable<GameObject> Spawned => _spawned;

    public event Action<GameObject> OnSpawned;

    private void Awake()
    {
        _spawned = new();
    }

    private void Update()
    {
        _spawned = _spawned.Where(s => (bool) s).ToList();
        if (IsFinished)
        {
            if (!_spawned.Any())
            {
                Game.Instance.DoGameWin();
                GameObject.Destroy(this);
            }
        }
        else
        {
            _time += Time.deltaTime;
            if (_currentIndex == -1 ||  _time >= _prefabs[_currentIndex].Time)
            {
                _time = 0f;
                _currentIndex++;
                if (IsFinished)
                    return;

                // Instanciate
                GameObject prefab = _prefabs[_currentIndex].Prefab;
                if (prefab)
                {
                    GameObject instanciated = GameObject.Instantiate(prefab, parent: transform, position: transform.position, rotation: transform.rotation);
                    _spawned.Add(instanciated);
                    OnSpawned?.Invoke(instanciated);
                }
            }
        }
    }

    public int GetTotalToSpawnCount()
    {
        return _prefabs.Count(p => (bool) p.Prefab);
    }
}
