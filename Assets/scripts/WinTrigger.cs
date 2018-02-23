using UnityEngine;

/// <summary>
/// WinTrigger Description
/// </summary>
public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        kangooscript player = other.GetComponent<kangooscript>();
        if(player)
        {
            GameStateManager.GetSingleton.ChangeState(GameState.Victory);
        }
    }
}
