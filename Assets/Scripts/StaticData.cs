using UnityEngine;
public class StaticData {
    public static float Vol = 0.5f;
    public static int coins = 0;
    public static int life = 3;
    public static void setVol(float val)
    {
        if (val >= 0 && val <= 1)
        {
            StaticData.Vol = val;
        }
    }

    public static float getVol() { return Vol; }

    public static void addCoins(int val)
    {   
            coins += val;
    }

    public static int getCoins() { return coins; }

    public static void addLife(int val)
    {
        life += val;
    }
    public static void setLife(int val)
    {
        life = val;
    }

    public static void setCoins(int val)
    {
        coins = val;
    }

    public static void resetStats()
    {
        life = 3;
        coins = 0;
    }

    public static int getLife() { return life; }
}