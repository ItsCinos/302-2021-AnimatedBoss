using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorpionTailController : MonoBehaviour
{

    public Transform tailController;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = Mathf.Sin(Time.time) * 10;
        tailController.localRotation = Quaternion.AngleAxis(angle, Vector3.right);
    }
}
