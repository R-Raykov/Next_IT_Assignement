using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientateObject : MonoBehaviour
{
    private RaycastHit hit;

    private LayerMask mask;

    private void OnEnable()
    {
        //mask = LayerMask.GetMask("Wall");
        //print(mask);
        //RaycastHit _hit = GetClosestWall(transform.position);
        //transform.parent = _hit.transform;
        //print(_hit.transform.name);
        //transform.position = _hit.point;
        //transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    private RaycastHit GetClosestWall(Vector3 pOrigin)
    {
        RaycastHit _hit = new RaycastHit();

        if (Physics.Raycast(pOrigin, transform.forward, out hit, 100, layerMask:1 << 9))
        {
            if (_hit.distance > hit.distance)
                _hit = hit;
        }

        if (Physics.Raycast(pOrigin, -transform.forward, out hit, 100, layerMask: 1 << 9))
        {
            if (_hit.distance > hit.distance)
                _hit = hit;
        }

        if (Physics.Raycast(pOrigin, transform.right, out hit, 100, layerMask: 1 << 9))
        {
            if (_hit.distance > hit.distance)
                _hit = hit;
        }

        if (Physics.Raycast(pOrigin, -transform.right, out hit, 100, layerMask: 1 << 9))
        {
            if (_hit.distance > hit.distance)
                _hit = hit;
        }

        return _hit;
    }
}
