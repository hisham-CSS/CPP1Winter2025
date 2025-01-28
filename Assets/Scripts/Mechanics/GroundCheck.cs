using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    //groundcheck variables
    [SerializeField, Range(0.01f, 0.1f)] private float groundCheckRadius = 0.02f;
    [SerializeField] private LayerMask isGroundLayer;
    private Transform groundCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //groundcheck initalization
        GameObject newGameObject = new GameObject();
        newGameObject.transform.SetParent(transform);
        newGameObject.transform.localPosition = Vector3.zero;
        newGameObject.name = "GroundCheck";
        groundCheck = newGameObject.transform;
    }

    public bool isGrounded()
    {
        if (!groundCheck) return false;
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
    }
}
