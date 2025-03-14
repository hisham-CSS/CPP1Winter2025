using Unity.VisualScripting;
using UnityEngine;

//This script is fine for a small scope - Pickups that remain fairly similar in their usecase - but anything more than 10 pickups and varied mechanics for the pickups, will probably require a different solution.
public class Pickups : MonoBehaviour
{
    public enum PickupType
    {
        Life,
        Powerup,
        Score
    }

    public PickupType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (type)
            {
                case PickupType.Life:
                    GameManager.Instance.lives++;
                    break;
                case PickupType.Powerup:
                    GameManager.Instance.PlayerInstance.SpeedChange();
                    break;
                case PickupType.Score:
                    GameManager.Instance.score++;
                    break;
            }

            Destroy(gameObject);
        }
    }
}
