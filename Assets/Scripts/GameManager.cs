using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<PlayerController> allPlayers;
    private Vector3 moveDirectionThisFrame = Vector3.zero;

    private PlayerInputHandler playerInputHandler;

    private void Awake()
    {
        playerInputHandler = GetComponent<PlayerInputHandler>();
        if (allPlayers == null) allPlayers = new List<PlayerController>();
    }

    private void Update()
    {
        if (IsAnyPlayerMoving()) return;

        moveDirectionThisFrame = Vector3.zero;

        if (playerInputHandler.Forward) { moveDirectionThisFrame = Vector3.forward; playerInputHandler.Forward = false; }
        else if (playerInputHandler.Backward) { moveDirectionThisFrame = Vector3.back; playerInputHandler.Backward = false; }
        else if (playerInputHandler.Left) { moveDirectionThisFrame = Vector3.left; playerInputHandler.Left = false; }
        else if (playerInputHandler.Right) { moveDirectionThisFrame = Vector3.right; playerInputHandler.Right = false; }

        if (moveDirectionThisFrame != Vector3.zero)
        {
            for (int i = 0; i < allPlayers.Count; i++)
            {
                var player = allPlayers[i];
                if (player == null) continue;

                player.ReceiveMovementCommand(moveDirectionThisFrame);
            }
        }
    }

    public void FreezeSelectedPlayer(int index)
    {
        if (index >= 0 && index < allPlayers.Count && allPlayers[index] != null)
            allPlayers[index].FreezePlayer();
    }

    public void ReverseSelectedPlayer(int index)
    {
        if (index >= 0 && index < allPlayers.Count && allPlayers[index] != null)
            allPlayers[index].ReversePlayer();
    }

    private bool IsAnyPlayerMoving()
    {
        foreach (var player in allPlayers)
        {
            if (player != null && player.GetIsMoving()) return true;
        }
        return false;
    }
}
