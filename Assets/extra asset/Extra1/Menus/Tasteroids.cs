using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasteroids : MonoBehaviour
{
    public GameObject brokenAsteroid;
    private float rotationSpeed;
    private Rigidbody2D rb;
    public float speed;
    private Vector2 direction;
    public Transform tship;
    private float shift;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed=Random.Range(-25,25);

        rb=GetComponent<Rigidbody2D>();
        direction=tship.transform.position-transform.position;
        shift=Random.Range(-3,3);
        rb.AddForce(new Vector2(direction.x+shift,direction.y+shift)*speed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0,rotationSpeed)*Time.deltaTime);
        CheckPosition();
    }
    private void CheckPosition()
    {

        float maxX = 12;
        float maxY = 8;

        if (transform.position.x > maxX || transform.position.x < -maxX)
        {
            Destroy(gameObject);
        }
         if (transform.position.y > maxY || transform.position.y < -maxY)
        {
            Destroy(gameObject);
        }

    }
    private void OnCollisionEnter2D(Collision2D col) 
    {
            Instantiate(brokenAsteroid,transform.position,Quaternion.identity);            
            Destroy(gameObject); 
        
    }
}
