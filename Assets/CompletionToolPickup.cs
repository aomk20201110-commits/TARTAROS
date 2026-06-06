using UnityEngine;

public class CompletionToolPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();

        if (player == null)
        {
            return;
        }

        player.ObtainCompletionTool();
        gameObject.SetActive(false);
    }
}