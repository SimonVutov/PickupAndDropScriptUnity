using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAndDrop : MonoBehaviour
{
    public GameObject camera;
    float maxPickupDistance = 5;
    GameObject itemCurrentlyHolding;
    bool isHolding = false;

    void Update()
    {
        if (Input.GetKeyDown("e")) Pickup();
        if (Input.GetKeyDown("q")) Drop();
    }

    void Pickup ()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxPickupDistance))
        {
            if (hit.transform.tag == "Item")
            {
                if (isHolding) Drop();

                itemCurrentlyHolding = hit.transform.gameObject;

                foreach (var c in hit.transform.GetComponentsInChildren<Collider>()) if (c != null) c.enabled = false;
                foreach (var r in hit.transform.GetComponentsInChildren<Rigidbody>()) if (r != null) r.isKinematic = true;

                itemCurrentlyHolding.transform.parent = transform;
                itemCurrentlyHolding.transform.localPosition = Vector3.zero;
                itemCurrentlyHolding.transform.localEulerAngles = Vector3.zero;

                isHolding = true;
            }
        }
    }

    void Drop ()
    {
        itemCurrentlyHolding.transform.parent = null;
        foreach (var c in itemCurrentlyHolding.GetComponentsInChildren<Collider>()) if (c != null) c.enabled = true;
        foreach (var r in itemCurrentlyHolding.GetComponentsInChildren<Rigidbody>()) if (r != null) r.isKinematic = false;
        isHolding = false;
        RaycastHit hitDown;
        Physics.Raycast(transform.position, -Vector3.up, out hitDown);

        itemCurrentlyHolding.transform.position = hitDown.point + new Vector3(transform.forward.x, 0, transform.forward.z);
    }
}
