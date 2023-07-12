using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AirDefence : MonoBehaviour
{
    [SerializeField] private CapsuleCollider _attackRadius;
    [SerializeField] private Rocket _currentRocket;

    [SerializeField] private List<GameObject> _rockets;
    [SerializeField] private Transform _enemyTransform;
    [SerializeField] private int _currentRocketIndex = 0;

    [SerializeField] private Transform _rotateElement;
    [SerializeField] private Teams _team;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("RedTeam")&& !other.isTrigger)
        {
            Debug.Log("Detected");
            
                _currentRocket = _rockets[_currentRocketIndex].GetComponent<Rocket>();           
                _currentRocket.SetTarget(other.transform.GetChild(0).transform);         
                _currentRocketIndex++;

        }
    }
}
