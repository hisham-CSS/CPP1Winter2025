using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject[] prefabList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int rand = Random.Range(0, prefabList.Length);
        if (prefabList[rand] == null) return;

        Instantiate(prefabList[rand], transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
