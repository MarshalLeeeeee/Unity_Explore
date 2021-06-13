using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPos : MonoBehaviour
{
    public Vector3 posRange;
    public float switchTime;
    private Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        StartCoroutine(RandPos());
    }

    IEnumerator RandPos()
    {
        while (true)
        {
            transform.position = origin + new Vector3(Random.Range(-posRange.x, posRange.x), Random.Range(-posRange.y, posRange.y), Random.Range(-posRange.z, posRange.z));
            yield return new WaitForSeconds(switchTime);
        }
    }
}
