using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    private static readonly int GotHit = Animator.StringToHash("gotHit");
    
    private Rigidbody2D _body;
    private Animator _animator;

    private GameObject _player;
    
    private int _speed = 3;
    private bool _isMoving = true;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        
        _player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            _body.MovePosition(Vector2.MoveTowards(transform.position, _player.transform.position,
                _speed * Time.deltaTime));
        }

        _isMoving = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            _isMoving = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Sword"))
        {
            _animator.SetBool(GotHit, true);
            Destroy(gameObject, 1f);
        }
    }
}
