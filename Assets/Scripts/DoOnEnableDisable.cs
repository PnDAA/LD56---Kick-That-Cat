using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoOnEnableDisable : MonoBehaviour
{
    [SerializeField] UnityEvent _onEnable;
    [SerializeField] UnityEvent _onDisable;

    private void OnEnable()
    {
        _onEnable?.Invoke();
    }

    private void OnDisable()
    {
        _onDisable?.Invoke();
    }
}
