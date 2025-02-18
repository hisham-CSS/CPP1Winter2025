using UnityEngine;

public class TurretEnemy : Enemy
{
    [SerializeField] private float projectileFireRate = 2.0f;
    private float timeSinceLastFire = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        if (projectileFireRate <= 0)
            projectileFireRate = 2;
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        if (curPlayingClips[0].clip.name.Contains("Idle")) CheckFire();
     
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
