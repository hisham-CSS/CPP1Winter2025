using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public abstract class Enemy : MonoBehaviour
{
    //private - cannot be accessed outside of this class - properties that are internal to the class - not even subclasses can access these variables
    //public - it's a party and everyone is invited.  If a class holds a instanstiated reference of this class - it can access the variable as it pleases! Static variables don't require a class instance.
    //protected - private but also accessable by child classes

    protected SpriteRenderer sr;
    protected Animator anim;
    protected int health;
    [SerializeField] protected int maxHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (maxHealth <= 0) maxHealth = 5;

        health = maxHealth;
    }

    public virtual void TakeDamage(int DamageValue, DamageType damageType = DamageType.Default)
    {
        health -= DamageValue;

        if (health <= 0)
        {
            anim.SetTrigger("Death");

            if (transform.parent != null) Destroy(transform.parent.gameObject, 0.5f);
            else Destroy(gameObject, 0.5f);
        }
    }
}

public enum DamageType
{
    Default,
    JumpedOn
}
