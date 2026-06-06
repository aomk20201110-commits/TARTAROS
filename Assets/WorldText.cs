using UnityEngine;

public class WorldText : MonoBehaviour
{
    [TextArea(2, 5)]
    public string message = "The city descends, but the exit is above.";

    private bool playerNearby;
    private bool textOpen;

    private void Update()
    {
        if (!playerNearby)
        {
            textOpen = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            textOpen = !textOpen;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();

        if (player == null)
        {
            return;
        }

        playerNearby = true;
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();

        if (player == null)
        {
            return;
        }

        playerNearby = false;
        textOpen = false;
    }

    private void OnGUI()
    {
        if (!playerNearby)
        {
            return;
        }

        if (!textOpen)
        {
            GUI.Label(new Rect(30, 140, 500, 40), "Press E to read.");
            return;
        }

        GUI.Box(new Rect(30, 140, 720, 100), message);
    }
}