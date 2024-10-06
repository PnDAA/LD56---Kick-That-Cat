using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DoOnSpace : MonoBehaviour
{
    private Inputs _playerInputs;

    [SerializeField] private UnityEvent _onSpace;

    private void OnEnable()
    {
        _playerInputs = new();
        _playerInputs.MyUI.Validate.performed += OnSpace;
        _playerInputs.MyUI.Enable();
    }

    private void OnSpace(InputAction.CallbackContext context)
    {
        _onSpace?.Invoke();
    }

    private void OnDisable()
    {
        _playerInputs?.Dispose();
    }
}
