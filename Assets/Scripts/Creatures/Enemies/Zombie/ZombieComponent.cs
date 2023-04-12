using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieComponent : MonoBehaviour
{
    [SerializeField] ParticleSystem _hitParticle;

    private Rigidbody2D _rb;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
    }
    public void PlayHitParticle()
    {
        Vector3 rotationVector;
        Quaternion rotation;

        rotationVector = new Vector3(0f, 0f, 0f);
        rotation = Quaternion.Euler(rotationVector);
        _hitParticle.gameObject.transform.rotation = rotation;

        var velocity = _rb.velocity.x;
        var direction = velocity > 0 ? 1 : -1;

        if (velocity == 0)
        {
            rotationVector = new Vector3(0f, 0f, -72f);
            rotation = Quaternion.Euler(rotationVector);
            _hitParticle.gameObject.transform.rotation = rotation;
        }

        _hitParticle.gameObject.transform.localScale = new Vector3(direction, 1f, 1f);
        _hitParticle.gameObject.SetActive(true);
        _hitParticle.Play();       
    }
}
