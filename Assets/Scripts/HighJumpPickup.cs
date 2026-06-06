using UnityEngine;

public class HighJumpPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();

        if (player == null)
        {
            return;
        }

        player.EnableHighJump();
        gameObject.SetActive(false);
    }
}