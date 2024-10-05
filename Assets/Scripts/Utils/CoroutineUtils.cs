using System;
using System.Collections;
using UnityEngine;

public static class CoroutineUtils
{
    public static Coroutine StartCoroutineDoAfterXSec(this MonoBehaviour mb, float sec, Action toExecute)
    {
        return mb.StartCoroutine(DoAfterXSec(sec, toExecute));
    }

    private static IEnumerator DoAfterXSec(float sec, Action toExecute)
    {
        yield return new WaitForSeconds(sec);
        toExecute();
    }

    // !!! if game is pause it will be executed. 
    public static Coroutine StartCoroutineDoAfterXSecUnscale(this MonoBehaviour mb, float sec, Action toExecute)
    {
        return mb.StartCoroutine(DoAfterXSecUnscale(sec, toExecute));
    }

    public static IEnumerator DoAfterXSecUnscale(float sec, Action toExecute)
    {
        yield return new WaitForSecondsRealtime(sec);
        toExecute();
    }

    public static Coroutine StartCoroutineDoAtTheEndOfFrame(this MonoBehaviour mb, Action toExecute)
    {
        return mb.StartCoroutine(DoAtTheEndOfFrame(toExecute));
    }

    public static IEnumerator DoAtTheEndOfFrame(Action toExecute)
    {
        yield return new WaitForEndOfFrame();
        toExecute();
    }

    public static void RepeatEveryXSeconds(this MonoBehaviour mb, float delay, float repeat, Action toExecute)
    {
        mb.StartCoroutine(RepeatAction(delay, repeat, toExecute));
    }

    private static IEnumerator RepeatAction(float delay, float repeat, Action toExecute)
    {
        yield return new WaitForSeconds(delay);
        while (true)
        {
            toExecute();
            yield return new WaitForSeconds(repeat);
        }
    }
}
