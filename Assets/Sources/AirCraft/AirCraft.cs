using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AirCraft : MonoBehaviour
{

    [SerializeField] private Transform tr;
    void Start()
    {
       NavMeshAgent agent = GetComponent<NavMeshAgent>();  
       agent.destination = tr.position;
    }

}
