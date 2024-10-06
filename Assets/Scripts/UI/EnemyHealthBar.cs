using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        _enemy.OnHealthRemoved += OnHealthRemoved;
    }

    private void OnHealthRemoved()
    {
        _slider.gameObject.SetActive(true);
        _slider.value = _enemy.GetHealthRatio();
    }
}
