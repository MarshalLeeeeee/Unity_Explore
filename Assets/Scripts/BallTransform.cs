using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTransform : MonoBehaviour
{
    public Vector3 scaleLocalChange;
    public Vector3 positionLocalChange;
    public Vector3 rotationLocalChange;

    // Start is called before the first frame update
    void Start()
    {
        print("World position: " + transform.position.ToString());
        print("Local position: " + transform.localPosition.ToString());
        print("Parent: " + transform.parent.name.ToString());
        print("Sibling number: " + transform.parent.childCount.ToString());
        print("Children number: " + transform.childCount.ToString());
        print("World position valdation from parent.transform: " + transform.parent.localToWorldMatrix.MultiplyPoint3x4(transform.localPosition));
        print("World position valdation from self.transform: " + transform.localToWorldMatrix.MultiplyPoint3x4(new Vector3(0.0f, 0.0f, 0.0f)));
        print("Local position valdation from parent.transform: " + transform.parent.worldToLocalMatrix.MultiplyPoint3x4(transform.position));
        print("Local position valdation from self.transform: " + Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale).MultiplyPoint3x4(new Vector3(0.0f, 0.0f, 0.0f)));
        foreach (Transform child in transform.parent)
        {
            print(child.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += scaleLocalChange;
        transform.Translate(positionLocalChange);
        transform.Rotate(rotationLocalChange);
    }
}
