using System;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    private static readonly int GotHit = Animator.StringToHash("gotHit");

    [SerializeField] private AudioClip sfxHit;
    
    private Rigidbody2D _body;
    private Animator _animator;
    private AudioSource _sfxPlayer;

    private GameObject _player;
    
    private int _speed = 3;
    private bool _isMoving = true;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sfxPlayer = GetComponent<AudioSource>();
        
        _player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _isMoving = false;
            _animator.SetBool(GotHit, true);
            Destroy(gameObject, 1f);
        }
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _body.MovePosition(Vector2.MoveTowards(transform.position, _player.transform.position,
                _speed * Time.deltaTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Sword"))
        {
            _animator.SetBool(GotHit, true);
            _isMoving = false;
            _sfxPlayer.PlayOneShot(sfxHit);
            
            Destroy(gameObject, 1f);
        }
    }
}
