using UnityEngine;

public class ExitGateTrigger : MonoBehaviour
{
    public GameObject exitObject;

    private PlayerController nearbyPlayer;

    private void Update()
    {
        if (nearbyPlayer == null)
        {
            return;
        }

        if (!Input.GetKeyDown(KeyCode.E))
        {
            return;
        }

        if (!nearbyPlayer.HasCompletionTool)
        {
            UnityEngine.Debug.Log("Exit is locked.");
            return;
        }

        exitObject.SetActive(false);
        UnityEngine.Debug.Log("Exit opened.");
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();

        if (player == null)
        {
            return;
        }

        nearbyPlayer = player;
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();

        if (player == null)
        {
            return;
        }

        if (nearbyPlayer == player)
        {
            nearbyPlayer = null;
        }
    }

    private void OnGUI()
    {
        if (nearbyPlayer == null)
        {
            return;
        }

        if (!nearbyPlayer.HasCompletionTool)
        {
            GUI.Label(new Rect(30, 130, 600, 40), "Exit is locked. Find the tool above.");
            return;
        }

        GUI.Label(new Rect(30, 130, 600, 40), "Press E to open Exit.");
    }
}