using UnityEngine;

public class StartLevel : MonoBehaviour
{
    [SerializeField] private Transform startPos;
    //Expression Body syntax for functions that can be one line of code
    void Start() => GameManager.Instance.InstantiatePlayer(startPos);
}
