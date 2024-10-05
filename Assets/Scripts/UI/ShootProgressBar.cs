using UnityEngine;
using UnityEngine.UI;

public class ShootProgressBar : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    [SerializeField] private GameObject _shootControl;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private Gradient _progressColor;

    private void Awake()
    {
        _hero.OnShootStartedEvent += OnShootStarted;
        _hero.OnShootStoppedEvent += OnShootStopped;
    }

    private void OnShootStarted()
    {
        _shootControl.gameObject.SetActive(false);
        _progressBar.gameObject.SetActive(true);
    }

    private void OnShootStopped()
    {
        _shootControl.gameObject.SetActive(true);
        _progressBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_progressBar.gameObject.activeInHierarchy)
        {
            float ratio = _hero.GetCurrentStrengthRatio();
            _progressBar.value = ratio;
            _progressBar.fillRect.GetComponent<Image>().color = _progressColor.Evaluate(ratio);
        }
    }
}
