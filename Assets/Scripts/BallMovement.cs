using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{

    Rigidbody2D _rigidbody2D;
    bool _moving;
    float _initialForce = 150.0f;
    float _speed = 7;
    Vector2 _velocity;
    int _wallBumpToSpeedUp = 4;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _moving = false;
    }

    private void FixedUpdate()
    {

        _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * _speed;
        _velocity = _rigidbody2D.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_moving && Input.GetAxis("Jump") > 0)
        {
            Debug.Log("FIRE!!!");
            _moving = true;
            _rigidbody2D.AddForce(new Vector2(0.0f, _initialForce));
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collision!!");
        if(collision.gameObject.tag.Equals("Floor"))
        {
            GameManager.Instance.FloorHit();
            return;
        }
        if(collision.gameObject.tag.Equals("Wall"))
        {
            _wallBumpToSpeedUp -= 1;
            if(_wallBumpToSpeedUp == 0)
            {
                _wallBumpToSpeedUp = 4;
                _speed += 0.5f;
            }
        }
        _rigidbody2D.velocity = Vector2.Reflect(_velocity, collision.GetContact(0).normal);
    }

    
}
