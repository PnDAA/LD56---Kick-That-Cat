using TMPro;
using UnityEngine;

public class SetRandomText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string[] _possibleTexts;

    private void OnEnable()
    {
        _text.text = _possibleTexts.TakeOneRandom();
    }
}
