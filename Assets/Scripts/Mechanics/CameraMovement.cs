using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float minXPos;
    [SerializeField] private float maxXPos;

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.PlayerInstance) return;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(GameManager.Instance.PlayerInstance.gameObject.transform.position.x, minXPos, maxXPos);
        transform.position = pos;
    }
}
