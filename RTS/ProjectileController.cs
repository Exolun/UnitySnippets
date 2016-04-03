using UnityEngine;
using System.Collections;
using System;

public class ProjectileController : MonoBehaviour
{
    /// <summary>
    /// Object with an explosion effect to instantiate on impact
    /// </summary>
    public GameObject Explosion;

    /// <summary>
    /// Units/second to travel at
    /// </summary>
    public float Velocity;

    /// <summary>
    /// Normalized direction of travel for the projectile
    /// </summary>
    public Vector3 Direction;

    /// <summary>
    /// Tag of object to consider an enemy (for impact logic)
    /// </summary>
    public string EnemyTag;

    /// <summary>
    /// Amount of time (in seconds) that the projectile exists before expiring if no impact is made
    /// </summary>
    public float Lifetime;

    private SphereCollider spCollider;
    private DateTime startTime;
    private TimeSpan timeToLive;

    void Start()
    {
        this.spCollider = this.GetComponent<SphereCollider>();
        this.startTime = DateTime.Now;
        this.timeToLive = TimeSpan.FromSeconds(this.Lifetime);
    }

    void Update()
    {
        if(DateTime.Now - startTime > timeToLive)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.transform.position = this.gameObject.transform.position + (this.Direction * Time.deltaTime * Velocity);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(this.EnemyTag))
        {
            var exp = Instantiate(this.Explosion);
            exp.transform.position = other.ClosestPointOnBounds(this.gameObject.transform.position);
            
            Destroy(exp, .5f);
            Destroy(this.gameObject);            
        }        
    }
}
