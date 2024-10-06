using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private float _volume = 1f;
    [SerializeField] private GameObject _audioSourcePrefab;
    private AudioSource[] _sources;

    private int _lastUsedIndex = 0;

    private void Awake()
    {
        const int count = 20;
        _sources = new AudioSource[count];
        for (int i = 0; i < count; i++)
        {
            GameObject instanciated = GameObject.Instantiate(_audioSourcePrefab, parent: transform);
            _sources[i] = instanciated.GetComponent<AudioSource>();
        }
    }

    public AudioSource Play(AudioClip audioClip, Vector3 position, float volume = 1f, float pitch = 1f)
    {
        AudioSource source = _sources[_lastUsedIndex];
        source.transform.position = position;
        source.clip = audioClip;
        source.pitch = pitch;
        source.volume = volume * _volume;
        source.Play();
        _lastUsedIndex = (_lastUsedIndex + 1) % _sources.Length;
        return source;
    }
}
