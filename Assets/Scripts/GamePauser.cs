using UnityEngine;

public class GamePauser : MonoBehaviour
{
    private void OnEnable()
    {
        Game.Instance.GamePauseManager.Pause();
    }

    private void OnDisable()
    {
        Game.Instance.GamePauseManager.Unpause();
    }
}
