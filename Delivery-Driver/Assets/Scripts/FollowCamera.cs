using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject car;
    public float offset = -10;
    void LateUpdate()
    {
        transform.position = new Vector3(car.transform.position.x, car.transform.position.y, offset);
    }
}
