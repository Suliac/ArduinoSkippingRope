using UnityEngine;

public enum GameState
{
    Playing,
    Victory,
    GameOver
}

/// <summary>
/// GameStateManager Description
/// </summary>
public class GameStateManager : MonoBehaviour
{
    private static GameStateManager instance;
    public static GameStateManager GetSingleton
    {
        get { return instance; }
    }

    private GameState currentState; // Etat actuel du jeu
    public GameState CurrentState { get { return currentState; } }

    private int score; // Score du joueur, TODO ?
    public GameObject WinPannel;
    public kangooscript player;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        ChangeState(GameState.Playing);
    }

    public void Update()
    {
        switch (currentState)
        {
            case GameState.Playing:
                break;
            case GameState.Victory:
                if (player.IsPressingLeftButton(true) || player.IsPressingRightButton(true))
                    ChangeState(GameState.Playing);
                break;
            case GameState.GameOver:
                break;
            default:
                break;
        }
    }

    public void ChangeState(GameState stateWanted)
    {
        switch (stateWanted)
        {
            case GameState.Playing:
                currentState = GameState.Playing;
                if (WinPannel)
                    WinPannel.SetActive(false);
                
                score = 0;
                player.Init();
                break;
            case GameState.Victory:
                currentState = GameState.Victory;
                if (WinPannel)
                    WinPannel.SetActive(true);

                break;
            case GameState.GameOver:
                break;
            default:
                break;
        }
    }
}
