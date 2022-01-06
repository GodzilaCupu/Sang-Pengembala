using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [Range(1f, 10f)]
    [SerializeField] private float smoothSpeed;
    [SerializeField] private float minX,maxX;
    [SerializeField] private float minZ,maxZ;
    [SerializeField] private Vector3 offset;
    private Vector3 velocity = Vector3.zero;


    float xBoundaries;
    float zBoundaries;
    void LateUpdate()
    {
        xBoundaries = Mathf.Clamp(target.position.x,minX, maxX);
        zBoundaries = Mathf.Clamp(target.position.z,minZ, maxZ);
        Vector3 followpos = new Vector3(xBoundaries+offset.x, this.gameObject.transform.position.y, zBoundaries + offset.z);
        Vector3 smoothPos = Vector3.SmoothDamp(transform.position, followpos, ref velocity, smoothSpeed * Time.deltaTime);

        transform.position = smoothPos;
    }

}
