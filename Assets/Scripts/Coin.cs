using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
   [SerializeField] private float coinDuration = 5f;

    private void Start()
    {
       gameObject.GetComponent<Collider2D>().enabled = true;
    }
    void Update()
    {
        if (coinDuration > 0)
        {
            coinDuration -= Time.deltaTime;
        }
        if (coinDuration <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
