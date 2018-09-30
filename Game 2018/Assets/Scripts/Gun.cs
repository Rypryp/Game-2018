using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public int Damage = 1;

    private ParticleSystem gunParticleSystem;
    private Animator animator;

    private void Start()
    {
        gunParticleSystem = GetComponentInChildren<ParticleSystem>();
        animator = GetComponent<Animator>();
    }

    public void OnFired()
    {
        gunParticleSystem.Emit(1);
        animator.SetTrigger("Fire");
    }
}
