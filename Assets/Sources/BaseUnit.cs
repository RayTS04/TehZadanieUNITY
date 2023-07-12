using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum Teams
{
    RedTeam =1,
    BlueTeam = 2,
    GreenTeam = 3,
    YellowTeam = 4
}

public class BaseUnit : MonoBehaviour , IDamagable
{
    [SerializeField] private CapsuleCollider _attackRadius;
    [SerializeField] private Transform tr;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Teams _team;
    [SerializeField] private int _health;

    [SerializeField] private Rocket _currentRocket;

    [SerializeField] private List<GameObject> _rockets;
    [SerializeField] private Transform _enemyTransform;
    [SerializeField] private int _currentRocketIndex = 0;

    [SerializeField] private ParticleSystem _blackSmoke;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent != null)
        {   
            _agent.destination = tr.position;
        }
        if (_attackRadius != null)
        {
            _attackRadius = GetComponent<CapsuleCollider>();
            Debug.Log("RadiusAssign");
        }
    }

    private void OnTriggerEnter(Collider other)
    {     
        if (other.gameObject.layer == LayerMask.NameToLayer("BlueTeam")&& !other.isTrigger)
        {
            Debug.Log("EnemyDetected");
            _currentRocket = _rockets[_currentRocketIndex].GetComponent<Rocket>();
            _currentRocket.SetTarget(other.transform.GetChild(0).transform);
            _currentRocketIndex++;
        }
    }
    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Death();
        }
    }
    private void Death()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        _blackSmoke.Play();
        gameObject.GetComponent<Animator>().SetTrigger("Death");
        Debug.Log("Death");
    }

}
