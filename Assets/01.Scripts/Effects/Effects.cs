using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Effects : PoolableMono
{
    ParticleSystem particle;

    public override void Init()
    {
        particle = GetComponent<ParticleSystem>();
        StartCoroutine(EffectLifeTime());
    }

    private IEnumerator EffectLifeTime()
    {
        yield return new WaitForSeconds(particle.main.duration + 0.2f);
        PoolManager.Instance.Push(this);
    }
}
