using System;

public static class Alea
{
    private static Random rnd;

    static Alea()
    {
        rnd = new Random();
    }

    public static int GetInt(int min, int max)
    {
        return rnd.Next(min, max);
    }

    public static float GetFloat(float min, float max)
    {
        return (float) rnd.NextDouble() * (max - min) + min;
    }
}

