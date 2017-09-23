using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FGravityBody : MonoBehaviour {

    public FGravityAttractor attractor;
    public Rigidbody objectBody;

    private void FixedUpdate()
    {
        if(attractor != null && objectBody != null)
            attractor.Attract(objectBody);
    }

    private void OnTriggerEnter(Collider other)
    {
        attractor = other.transform.parent.GetComponent<FGravityAttractor>();
    }
}
