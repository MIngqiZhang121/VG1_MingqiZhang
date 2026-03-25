using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        // Outlets
        Rigidbody2D _rigidbody2D;
        public Transform aimPivot;
        public GameObject projectilePrefab;
        SpriteRenderer _spriteRenderer;
        Animator animator;

        // State Tracking
        public int jumpsLeft;

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        /*void FixedUpdate()
        {
            animator.SetFloat("Speed", _rigidbody2D.velocity.magnitude);

            if (_rigidbody2D.velocity.magnitude > 0)
            {
            animator.speed = _rigidbody2D.velocity.magnitude / 3f;
            }
            else
            {
            animator.speed = 1f;
            }
     
        }*/
        void FixedUpdate()
        {
            float speed = Mathf.Abs(_rigidbody2D.velocity.x);
            
            if (speed < 0.05f)
            {
                speed = 0f;
            }

            animator.SetFloat("Speed", speed);

          
            if (speed > 0.05f)
            {
                animator.speed = speed / 3f;
            }
            else
            {
                animator.speed = 1f;
            }
        }

        void Update()
        {
            // Move Player Left/Right and Flip Sprite
            /*if (Input.GetKey(KeyCode.A))
            {
                _rigidbody2D.AddForce(Vector2.left * 18f * Time.deltaTime, ForceMode2D.Impulse);
                _spriteRenderer.flipX = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _rigidbody2D.AddForce(Vector2.right * 18f * Time.deltaTime, ForceMode2D.Impulse);
                _spriteRenderer.flipX = false;
            }*/
            if (Input.GetKey(KeyCode.A))
            {
                _rigidbody2D.AddForce(Vector2.left * 18f * Time.deltaTime, ForceMode2D.Impulse);
                _spriteRenderer.flipX = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _rigidbody2D.AddForce(Vector2.right * 18f * Time.deltaTime, ForceMode2D.Impulse);
                _spriteRenderer.flipX = false;
            }
            else
            {
                 
                _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
            }

            // Aim Toward Mouse
            Vector3 mousePosition = Input.mousePosition;
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

            float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
            float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

            aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

            // Shoot
            if (Input.GetMouseButtonDown(0))
            {
                GameObject newProjectile = Instantiate(projectilePrefab);
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = aimPivot.rotation;
            }

            // Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (jumpsLeft > 0)
                {
                    jumpsLeft--;
                    _rigidbody2D.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
                }
            }
            animator.SetInteger("JumpsLeft", jumpsLeft);
        }

        void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.9f);
                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit2D hit = hits[i];
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        jumpsLeft = 2;
                    }
                }
            }
        }
    } 
} 