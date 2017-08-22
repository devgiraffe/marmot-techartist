using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHitCollider : MonoBehaviour {

    private Hammer hammer;

    private void Awake()
    {
        hammer = GetComponentInParent<Hammer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Marmot")
        {
            hammer.SetTarget(collision.gameObject.GetComponent<Marmot>());
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!hammer.IsHitting)
        {
            hammer.SetTarget(null);
        }
    }

}
