using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem currentParticle;
    public ParticleSystem newParticle;

    List<ParticleCollisionEvent> particleCollisionEventList;

    private void Start()
    {
        particleCollisionEventList = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(currentParticle, other, particleCollisionEventList);

        foreach (ParticleCollisionEvent collisionEvent in particleCollisionEventList)
        {
            EmitAtLocation(collisionEvent);
        }
    }

    void EmitAtLocation(ParticleCollisionEvent _particleCollisionEvent)
    {
        newParticle.transform.position = _particleCollisionEvent.intersection;
        newParticle.transform.rotation = Quaternion.LookRotation(_particleCollisionEvent.normal);

        newParticle.Emit(1);
    }
}
