using UnityEngine;

public static class Utils
{
    public static void SetProgressBarValue(GameObject scaleObject, int value, int maxValue)
    {
        var oldScale = scaleObject.transform.localScale;
        scaleObject.transform.localScale = new Vector3((float)value / maxValue, oldScale.y, oldScale.z);
    }
}
