using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    [SerializeField] private HeroFoot _heroFoot;
    [SerializeField] private float _timeForFullStrength = 2f;
    [SerializeField] private Animator _animator;

    private Inputs _inputActions;

    private float _shootStartedTime = 0f;

    public event Action OnShootStartedEvent;
    public event Action OnShootStoppedEvent;

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
        _heroFoot.StopCatDetection(); // in case we start a new shoot with an animation that didn't stopped.

        _shootStartedTime = Time.time;
        OnShootStartedEvent?.Invoke();
        _animator.SetTrigger("prepare_shoot");
    }

    private void OnShootReleased(InputAction.CallbackContext context)
    {
        _heroFoot.StartCatDetection(GetCurrentStrengthRatio());
        OnShootStoppedEvent?.Invoke();
        _animator.SetTrigger("shoot");
    }

    public float GetCurrentStrengthRatio()
    {
        // yoyo
        float timeSinceShootStarted = Time.time - _shootStartedTime;
        float timeSinceStartedModuled = MathUtils.mod(timeSinceShootStarted, 2*_timeForFullStrength);
        float wantedtime = timeSinceStartedModuled;
        if (timeSinceStartedModuled >= _timeForFullStrength)
            wantedtime = 2f * _timeForFullStrength - timeSinceStartedModuled;
        return wantedtime / _timeForFullStrength;
    }

    public void OnShootFinished()
    {
        _heroFoot.StopCatDetection();
    }
}
