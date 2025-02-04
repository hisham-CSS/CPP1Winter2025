using UnityEngine;

public class Score : MonoBehaviour, IPickup
{
    public int scoreToAdd;
    public void Pickup(PlayerController player)
    {
        player.score += scoreToAdd;
        Destroy(gameObject);
    }
}
