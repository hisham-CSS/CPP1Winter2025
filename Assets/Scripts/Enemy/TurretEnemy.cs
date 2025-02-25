using UnityEngine;

public class TurretEnemy : Enemy
{
    [SerializeField] private float distThreshold = 5;
    [SerializeField] private float projectileFireRate = 2.0f;
    //[SerializeField] private Transform playerTransform;
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

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.PlayerInstance) return;

        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        sr.flipX = (transform.position.x > GameManager.Instance.PlayerInstance.gameObject.transform.position.x);

        CheckDistance(Mathf.Abs(GameManager.Instance.PlayerInstance.gameObject.transform.position.x - transform.position.x), curPlayingClips[0]); 
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
