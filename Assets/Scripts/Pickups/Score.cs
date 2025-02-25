using UnityEngine;

public class Score : MonoBehaviour, IPickup
{
    public int scoreToAdd;
    public void Pickup()
    {
        GameManager.Instance.score += scoreToAdd;
        Destroy(gameObject);
    }
}
