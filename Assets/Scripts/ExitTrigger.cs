using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    private bool reachedExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<CharacterController>() == null)
        {
            return;
        }

        reachedExit = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        UnityEngine.Debug.Log("Exit reached.");
    }

    private void OnGUI()
    {
        if (!reachedExit)
        {
            return;
        }

        GUI.Label(new Rect(30, 30, 500, 40), "You found the exit.");
    }
}