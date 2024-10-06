using UnityEngine;

public class GameRestarter : MonoBehaviour
{
    [SerializeField] private GameObject _toReinstanciate;
    private GameObject _copy;
    private GameObject _current;

    void Awake()
    {
        _current = _toReinstanciate;

        _toReinstanciate.gameObject.SetActive(false);
        _copy = GameObject.Instantiate(_toReinstanciate, transform);
        _toReinstanciate.gameObject.SetActive(true);
    }

    public void Restart()
    {
        GameObject.Destroy(_current);
        _current = GameObject.Instantiate(_copy, transform);
        _current.SetActive(true);
    }
}
