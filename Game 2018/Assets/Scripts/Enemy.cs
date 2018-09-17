using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public int Health;

    public float MovementSpeed = 2f;

    private Transform target;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        target = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        //transform.LookAt(target);
        //transform.Translate(Vector3.forward * MovementSpeed * Time.deltaTime);
        navMeshAgent.SetDestination(target.position);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if(Health <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }
}
