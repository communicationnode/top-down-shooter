using UnityEngine;


public sealed class ExplodeParticleBased : MonoBehaviour
{
    [SerializeField] private byte   damage = 10;
    [SerializeField] private float  radius = 2;

    private void Start()
    {
        ParticleSystem particles = GetComponent<ParticleSystem>();
        Destroy(gameObject, particles.main.duration);

        foreach (Collider collider in Physics.OverlapSphere(transform.position,radius))
        {
            Debug.DrawRay(collider.transform.position, Vector3.up, Color.white, 2);

            if (collider.TryGetComponent<HPModule>(out HPModule hpModule))
            {
                hpModule.Damage((sbyte)damage);
            }
        }
    }
}
