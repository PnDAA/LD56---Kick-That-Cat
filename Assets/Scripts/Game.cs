using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;

    [SerializeField] public GameRestarter GameRestarter;

    private void Awake()
    {
        Instance = this;
    }
}
