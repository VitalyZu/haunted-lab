using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieComponent : Creature
{
    [SerializeField] ParticleSystem _hitParticle;
    [SerializeField] CheckCircleOverlap _attackCheck;
    [SerializeField] LayerCheck _attackLayerCheck;
    [SerializeField] SpawnListComponent _spawnListComponent;
    [SerializeField] AudioClip[] _hitClips;
    [SerializeField] LayerCheck _obstacleCheck;
    //[SerializeField] float _debugVelocityTime;
    private bool _canJump = true;

    private EnterTriggerComponent _obstacleTrigger;
    private void Start()
    {
        //_obstacleCheck.GetComponent<CircleCollider2D>().radius = _obstacleCheck.GetComponent<CircleCollider2D>().radius * gameObject.transform.parent.localScale.y;
        //_obstacleTrigger = _obstacleCheck.GetComponent<EnterTriggerComponent>();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (Time.frameCount % 3 == 0 && _isGrounded && _canJump)
        {
            RaycastHit2D[] hit = new RaycastHit2D[1];
            //Physics2D.Raycast(_collider.bounds.center, Vector2.right * Mathf.Sign(transform.localScale.x), _collider.bounds.extents.x + 0.11f, _attackMask);
            Physics2D.RaycastNonAlloc(_collider.bounds.center, Vector2.right * Mathf.Sign(transform.localScale.x), hit, _collider.bounds.extents.x + 0.3f, _groundMask);
            //RaycastHit2D hit = Physics2D.Raycast(_collider.bounds.center, Vector2.right  * Mathf.Sign(transform.localScale.x), _collider.bounds.extents.x + 0.3f, _groundMask);
            //if (hit.collider != null)
            if(hit[0].collider != null)
            {
                _canJump = false;
                _rb.AddForce(new Vector2(40f * Mathf.Sign(transform.localScale.x), 60f), ForceMode2D.Impulse);
                float radius = _attackCheck.Radius;
                //_attackCheck.Radius = 1f;
                //_attackCheck.Check();
                //_attackCheck.Radius = radius;
                StartCoroutine(SetObstacleTrigger());
                
            }
        }
    }

    private IEnumerator SetObstacleTrigger()
    {
        //_rb.AddForce(new Vector2(455f * Mathf.Sign(transform.localScale.x), 0f), ForceMode2D.Impulse);
        //yield return new WaitForSeconds(_debugVelocityTime);
        //_rb.AddForce(new Vector2(455f * Mathf.Sign(transform.localScale.x), 0f), ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        _canJump = true;
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


    public void MakeAttack()
    {
        _attackCheck.Check();
    }

    public void PlayHitSound()
    {
        var clipNum = Random.Range(0, _hitClips.Length);
        _audio.PlayOneShot(_hitClips[clipNum]);
    }
}
