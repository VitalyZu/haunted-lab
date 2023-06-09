using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private float _explosionTime;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private AudioClip _explosionClip;

    private Rigidbody2D _rb;
    private SpriteAnimation _animation;
    private DealHealthComponent _dealHealth;
    private AudioSource _audio;
    public void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animation = GetComponent<SpriteAnimation>();
        _dealHealth = GetComponent<DealHealthComponent>();
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(_explosionTime);
        DoExplosion();
        StartCoroutine(Calculate());
    }
    private void DoExplosion()
    {
        _rb.freezeRotation = true;
        transform.localScale = Vector2.one * 2;
        _audio.PlayOneShot(_explosionClip);
        _animation.SetAnimationByName("explosion");
        _particle.gameObject.SetActive(true);
        _particle.Play();
    }

    private IEnumerator Calculate()
    {
        var collision = Physics2D.OverlapCircleAll(transform.position, 2f);
        for (int i = 0; i < collision.Length; i++)
        {
            var go = collision[i].gameObject;

            _dealHealth.DealHealth(go);
        }

        yield return null;

        collision = Physics2D.OverlapCircleAll(transform.position, 2f);

        for (int i = 0; i < collision.Length; i++)
        {
            var go = collision[i].gameObject;
            var rb = go.GetComponent<Rigidbody2D>();
            var pd = go.GetComponent<ProbabilityDestroyedComponent>();

            if (rb != null)
            {
                var direction = go.transform.position - transform.position;
                var vector = direction.normalized * 5f;
                rb.AddForce(vector, ForceMode2D.Impulse);
            }
            if (pd != null && pd.enabled)
            {
                pd.ForceDestroy = true;
                pd.Check();
            }
        }
    }
}
