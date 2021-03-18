using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colorfly : MonoBehaviour
{
    private static readonly int MovingLeft = Animator.StringToHash("movingLeft");
    private static readonly int MovingRight = Animator.StringToHash("movingRight");
    
    [SerializeField] private Text orbCountText;
    [SerializeField] private List<AudioClip> pickupSounds;

    private Rigidbody2D _body;
    private Animator _animator;
    private AudioSource _sfxPlayer;

    private const int Speed = 700;
    private int _orbs = 0;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sfxPlayer = GetComponent<AudioSource>();

        orbCountText.text = $"{_orbs}";
    }

    private void FixedUpdate()
    {
        var horizontalVelocity = Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime;
        var verticalVelocity = Input.GetAxisRaw("Vertical") * Speed * Time.deltaTime;

        _body.velocity = new Vector2(horizontalVelocity, verticalVelocity);

        if (horizontalVelocity < 0)
        {
            _animator.SetBool(MovingLeft, true);
            _animator.SetBool(MovingRight, false);
        }
        else if (horizontalVelocity > 0)
        {
            _animator.SetBool(MovingLeft, false);
            _animator.SetBool(MovingRight, true);
        }
        else
        {
            _animator.SetBool(MovingLeft, false);
            _animator.SetBool(MovingRight, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Orb"))
        {
            _orbs++;
            orbCountText.text = $"{_orbs}";
            _sfxPlayer.PlayOneShot(pickupSounds[_orbs % pickupSounds.Count]);
            
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.Equals("Tree"))
        {
            
        }
    }
}
