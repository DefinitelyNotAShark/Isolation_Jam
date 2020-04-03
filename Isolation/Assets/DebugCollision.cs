using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCollision : MonoBehaviour
{
    [SerializeField]
    private float enemyDamageAmount = 10;

    private ParticleSystem system;
    private ParticleCollisionEvent[] CollisionEvents;
    // Start is called before the first frame update
    void Start()
    {
        system = GetComponent<ParticleSystem>();
        CollisionEvents = new ParticleCollisionEvent[8];
    }

    public void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Player")) {
            Player player = other.GetComponent<Player>();

            int collCount = system.GetSafeCollisionEventSize();

            if (collCount > CollisionEvents.Length)
                CollisionEvents = new ParticleCollisionEvent[collCount];

            int eventCount = system.GetCollisionEvents(other, CollisionEvents);

            for (int i = 0; i < eventCount; i++)
            {
                player.TakeDamage(enemyDamageAmount);//
            }
        }
    }
}

