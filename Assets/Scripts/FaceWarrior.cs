using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceWarrior : MonoBehaviour
{
    public float senseRadius;
    private GameObject warrior;
    private Transform warriorTransform;

    // Start is called before the first frame update
    void Start()
    {
        warrior = GameObject.FindWithTag("Player");
        warriorTransform = warrior.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relative =  warriorTransform.position - transform.position;
        if (Mathf.Abs(relative.magnitude) < senseRadius)
        {
            Vector3 relativeProj = Vector3.ProjectOnPlane(relative, transform.up).normalized;
            transform.LookAt(transform.position + relativeProj);
        }
    }
}
