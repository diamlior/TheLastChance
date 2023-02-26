using UnityEngine;
public class StaticData {
    public static float Vol = 0.5f;
    public static void setVol(float val)
    {
        if (val >= 0 && val <= 1)
        {
            StaticData.Vol = val;
        }
            

    }

    public static float getVol() { return Vol; }
}