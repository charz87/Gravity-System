using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FGravityAttractor : MonoBehaviour {

    public float gravity = -10f;
    public Vector3 gravityCenter = Vector3.zero;

    public bool bDistanceStrengthens = false;

    public void Attract(Rigidbody attractedBody)
    {
        Vector3 gravityUp = (attractedBody.transform.position - transform.position).normalized;
        Vector3 attractedBodyUp = attractedBody.transform.up;

        if (attractedBody.gameObject.GetComponent<PlayerController>() != null && attractedBody.gameObject.GetComponent<PlayerController>().grounded)
        {
            print("Piso");
            Vector3 pullVector = FindSurface(attractedBody);
            OrientBody(attractedBody, pullVector);
            attractedBody.AddForce(pullVector * gravity);
        }
        else
        {

            OrientBody(attractedBody, gravityUp);
           
        }

        attractedBody.AddForce(gravityUp * gravity);






    }
        

    private Vector3 FindSurface(Rigidbody attractedBody)
    {
        float distance = Vector3.Distance(this.transform.position, attractedBody.transform.position);
        Vector3 surfaceNormal = Vector3.zero;

        RaycastHit hit;

        if(Physics.Raycast(attractedBody.transform.position, -attractedBody.transform.up, out hit, distance))
        {
            surfaceNormal = hit.normal;
        }

        return surfaceNormal;
    }

    void OrientBody(Rigidbody attractedBody, Vector3 surfaceNormal)
    {
        Quaternion targetRotation = Quaternion.FromToRotation(attractedBody.transform.up, surfaceNormal) * attractedBody.rotation; // get to desired Rotation 
        attractedBody.rotation = Quaternion.Lerp(attractedBody.rotation, targetRotation, 50f * Time.deltaTime);
        //attractedBody.transform.localRotation = Quaternion.FromToRotation(attractedBody.transform.up, surfaceNormal) * attractedBody.rotation;
    }
}
