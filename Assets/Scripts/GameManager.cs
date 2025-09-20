using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<PlayerController> allPlayers;
    public GameObject levelFailedPanel;
    public TextMeshProUGUI moveCountText;
    public int moveCount = 10;

    private Vector3 moveDirectionThisFrame = Vector3.zero;

    private PlayerInputHandler playerInputHandler;

    private void Awake()
    {
        Instance = this;

        playerInputHandler = GetComponent<PlayerInputHandler>();
        if (allPlayers == null) allPlayers = new List<PlayerController>();

        moveCountText.text = moveCount.ToString();
    }

    private void Update()
    {
        if (IsAnyPlayerMoving()) return;

        moveDirectionThisFrame = Vector3.zero;

        if (moveCount > 0)
        {
            if (playerInputHandler.Forward)
            {
                moveDirectionThisFrame = Vector3.forward;
                playerInputHandler.Forward = false;

                moveCount--;
                moveCountText.text = moveCount.ToString();
            }
            else if (playerInputHandler.Backward)
            {
                moveDirectionThisFrame = Vector3.back;
                playerInputHandler.Backward = false;

                moveCount--;
                moveCountText.text = moveCount.ToString();
            }
            else if (playerInputHandler.Left)
            {
                moveDirectionThisFrame = Vector3.left;
                playerInputHandler.Left = false;

                moveCount--;
                moveCountText.text = moveCount.ToString();
            }
            else if (playerInputHandler.Right)
            {
                moveDirectionThisFrame = Vector3.right;
                playerInputHandler.Right = false;

                moveCount--;
                moveCountText.text = moveCount.ToString();
            }
        }
        else
        {
            levelFailedPanel.SetActive(true);
        }

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
