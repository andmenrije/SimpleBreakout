using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{

    Rigidbody2D _rigidBody2D;
    float _velocity = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        float horizontalValue = Input.GetAxis("Horizontal");
        if (horizontalValue < 0)
        {

            Transform current = _rigidBody2D.transform;
            float newX = current.localPosition.x - (_velocity * Time.deltaTime);
            _rigidBody2D.MovePosition(new Vector2(newX, current.position.y));

        }
        else if (horizontalValue > 0)
        {

            Transform current = _rigidBody2D.transform;
            float newX = current.position.x + (_velocity * Time.deltaTime);
            _rigidBody2D.MovePosition(new Vector2(newX, current.position.y));

        }
        else
        {

            // Stop the movement
        }

        //_rigidBody2D.MovePosition(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 0));

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Detected");
    }






}
