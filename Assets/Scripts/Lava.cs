using UnityEngine;

public class Lava : MonoBehaviour
{
    public GameObject levelFailedPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < GameManager.Instance.allPlayers.Count; i++)
            {
                GameManager.Instance.allPlayers[i].enabled = false;
            }

            levelFailedPanel.SetActive(true);
        }
    }
}
