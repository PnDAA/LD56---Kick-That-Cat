using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class Game : MonoBehaviour
{
    public static Game Instance;

    [SerializeField] public GameRestarter GameRestarter;
    [SerializeField] public GamePauseManager GamePauseManager;
    [SerializeField] public GameObject ToDisplayOnStart;
    [SerializeField] public GameObject ToDisplayOnGameOver;
    [SerializeField] public GameObject ToDisplayOnGameWin;
    [SerializeField] public SoundPlayer SoundPlayer;

    private void Awake()
    {
        Instance = this;
        if (!Application.isEditor)
            ToDisplayOnStart.gameObject.SetActive(true);
    }

    public void DoGameOver()
    {
        ToDisplayOnGameOver.gameObject.SetActive(true);
    }

    public void DoGameWin()
    {
        ToDisplayOnGameWin.gameObject.SetActive(true);  
    }
}
