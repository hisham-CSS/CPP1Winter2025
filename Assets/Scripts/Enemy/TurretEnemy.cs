using System;
using UnityEngine;

public class TurretEnemy : Enemy
{
    [SerializeField] private float distThreshold = 5;
    [SerializeField] private float projectileFireRate = 2.0f;
    
    private Transform playerTransform;
    private float timeSinceLastFire = 0;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        if (projectileFireRate <= 0)
            projectileFireRate = 2;

        if (distThreshold <= 0)
            distThreshold = 5;

    }

    private void OnEnable() 
    {
        GameManager.Instance.OnPlayerSpawned += OnPlayerSpawnedCallback;   
    }
    private void OnDisable()
    {
        GameManager.Instance.OnPlayerSpawned -= OnPlayerSpawnedCallback;
    }

    private void OnPlayerSpawnedCallback(PlayerController controller) => playerTransform = controller.transform;

    // Update is called once per frame
    void Update()
    {
        if (!playerTransform) return;

        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        sr.flipX = (transform.position.x > playerTransform.position.x);

        CheckDistance(Mathf.Abs(playerTransform.position.x - transform.position.x), curPlayingClips[0]); 
    }

    void CheckDistance(float distance, AnimatorClipInfo curClip)
    {
        if (distance <= distThreshold)
        {
            sr.color = Color.red;
            if (curClip.clip.name.Contains("Idle")) CheckFire();
        }
        else
            sr.color = Color.white;
    }

    void CheckFire()
    {
        if (Time.time >= timeSinceLastFire + projectileFireRate)
        {
            anim.SetTrigger("Fire");
            timeSinceLastFire = Time.time;
        }
    }
}
