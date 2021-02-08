using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActorTemplate
{
    int SendDamager();
    void TakeDamage(int incomingDamager);
    void Die();
    void ActorStats(SOActorModel actorModel);
}
