using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    [SerializeField] private HeroFoot _heroFoot;

    private Inputs _inputActions;

    private void Awake()
    {
        _inputActions = new Inputs();

        _inputActions.Player.Shoot.started += OnShootStarted;
        _inputActions.Player.Shoot.canceled += OnShootReleased;
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
    }

    private void OnShootStarted(InputAction.CallbackContext context)
    {
    }

    private void OnShootReleased(InputAction.CallbackContext context)
    {
        _heroFoot.SetCatDetection(true);

        // mimic animation
        this.StopAllCoroutines();
        this.StartCoroutineDoAfterXSec(1f, () => _heroFoot.SetCatDetection(false));
    }
}
