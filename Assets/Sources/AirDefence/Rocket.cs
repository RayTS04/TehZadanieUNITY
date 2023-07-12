using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Rocket : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private GameObject _explodeFX;
    [SerializeField] private int _damage = 100;

    private void Update()
    {
        if (_target == null)
            return;
        Vector3 direction = _target.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }
    public void SetTarget(Transform transform)
    {
        _target = transform;
        gameObject.GetComponent<ParticleSystem>().Play();
    }
    private void OnCollisionEnter(Collision collision)
    {      
            Debug.Log("Explode");
            GameObject explosion = Instantiate(_explodeFX, transform.position, Quaternion.identity);
            ParticleSystem explosionParticleSystem = explosion.GetComponent<ParticleSystem>();
            explosionParticleSystem.Play();
            float explosionDuration = explosionParticleSystem.main.duration;         
            IDamagable damageable = collision.collider.GetComponent<IDamagable>();
            if (damageable != null)
            {
                damageable.TakeDamage(_damage);
                Debug.Log("Damaged");
            }
            Destroy(explosion, explosionDuration);
           Destroy(gameObject);     
    }
}


