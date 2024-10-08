using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    [SerializeField] private HeroFoot _heroFoot;
    [SerializeField] private float _timeForFullStrength = 2f;
    [SerializeField] private Animator _animator;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _prepareSound;
    [SerializeField] private AudioClip[] _shootSound;

    private Inputs _inputActions;

    private float _shootStartedTime = 0f;

    private float _lastShootStrength;

    public event Action OnShootStartedEvent;
    public event Action OnShootStoppedEvent;

    private void Awake()
    {
        _inputActions = new Inputs();

        _inputActions.Player.Shoot.started += OnShootStarted;
        _inputActions.Player.Shoot.canceled += OnShootReleased;
    }

    private void OnDestroy()
    {
        _inputActions?.Dispose();
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
    }

    private void PlayRandomSound(AudioClip[] clips)
    {
        _audioSource.clip = clips.TakeOneRandom();
        _audioSource.volume = 0.05f;
        _audioSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        _audioSource.Play();
    }

    private void ResetTriggers()
    {
        _animator.ResetTrigger("shoot");
        _animator.ResetTrigger("prepare_shoot");
        _animator.ResetTrigger("stop");
    }

    private void OnShootStarted(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0)
            return;

        _heroFoot.StopCatDetection(); // in case we start a new shoot with an animation that didn't stopped.

        _shootStartedTime = Time.time;
        OnShootStartedEvent?.Invoke();
        ResetTriggers(); // to handle better animation cancel
        _animator.SetTrigger("stop"); // cancel on going shoot animation
        _animator.SetTrigger("prepare_shoot");
        PlayRandomSound(_prepareSound);
    }

    private void OnShootReleased(InputAction.CallbackContext context)
    {
        if (Time.timeScale == 0)
            return;

        OnShootStoppedEvent?.Invoke();
        ResetTriggers(); // to handle better animation cancel
        _animator.SetTrigger("shoot");
        _lastShootStrength = GetCurrentStrengthRatio();
        PlayRandomSound(_shootSound);
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

    public void OnAnimationStartDetection()
    {
        _heroFoot.StartCatDetection(_lastShootStrength);
    }

    public void OnAnimationStopDetection()
    {
        _heroFoot.StopCatDetection();
    }
}
