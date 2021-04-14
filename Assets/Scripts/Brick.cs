using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField]
    private int _life = 1;
    [SerializeField]
    private int _perHitScore = 0;
    [SerializeField]
    private int _score = 100;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {


        if(this.gameObject.tag.Equals("GreyBrick"))
        {
            return;
        }

        _life -= 1;
        if (_life <= 0)
        {

            Destroy(this.gameObject);
            
            GameManager.Instance.SetScore(GameManager.Instance.GetScore() + _score);
        }
        else
        {
            GameManager.Instance.SetScore(GameManager.Instance.GetScore() + _perHitScore);
        }
        
    }
}
