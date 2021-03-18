using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colorfly : MonoBehaviour
{
    private static readonly int MovingLeft = Animator.StringToHash("movingLeft");
    private static readonly int MovingRight = Animator.StringToHash("movingRight");
    private static readonly int TreeTriggered = Animator.StringToHash("treeTriggered");
    
    [SerializeField] private Text orbCountText;
    [SerializeField] private GameObject orbExplosionPrefab;
    [SerializeField] private List<AudioClip> pickupSounds;
    [SerializeField] private AudioClip sfxTree;
    [SerializeField] private AudioClip bgmColorfly1;
    [SerializeField] private AudioClip bgmColorfly2;
    [SerializeField] private AudioSource bgmPlayer;

    private Rigidbody2D _body;
    private Animator _animator;
    private AudioSource _sfxPlayer;

    private const int Speed = 700;
    private int _orbs = 0;
    private int _trees = 0;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sfxPlayer = GetComponent<AudioSource>();
        bgmPlayer.clip = bgmColorfly1;
        bgmPlayer.Play();

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

            var orbPos = other.gameObject.transform.position;
            
            Destroy(other.gameObject);
            Destroy(Instantiate(orbExplosionPrefab, orbPos, Quaternion.identity), 1.5f);
        }
        else if (other.gameObject.tag.Equals("Tree"))
        {
            var treeAnimator = other.GetComponent<Animator>();

            if (!treeAnimator.GetBool(TreeTriggered) && _orbs >= 3)
            {
                _trees++;
                treeAnimator.SetBool(TreeTriggered, true);
                _sfxPlayer.PlayOneShot(sfxTree);
                
                _orbs -= 3;
                orbCountText.text = $"{_orbs}";

                if (_trees >= 6)
                {
                    bgmPlayer.Stop();
                    bgmPlayer.clip = bgmColorfly2;
                    bgmPlayer.Play();
                }
            }
        }
    }
}
