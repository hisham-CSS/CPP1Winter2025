using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;

    private Transform playerTransform;

    private void OnEnable()
    {
        GameManager.Instance.OnPlayerSpawned += OnPlayerSpawnedCallback;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerSpawned -= OnPlayerSpawnedCallback;
    }

    void OnPlayerSpawnedCallback(PlayerController playerInstance) => playerTransform = playerInstance.transform;
  
    // Update is called once per frame
    void Update()
    {
        if (!playerTransform) return;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(playerTransform.position.x, minXPos, maxXPos);
        transform.position = pos;
    }
}
