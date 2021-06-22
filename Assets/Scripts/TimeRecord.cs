using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRecord
{
    public static float[] timeRecord = new float[4] { -1.0f, -1.0f, -1.0f, -1.0f};
    public static void changeRecord(int i, float t)
    {
        if (timeRecord[i] < 0.0f || timeRecord[i] > t) timeRecord[i] = t;
    }
}
