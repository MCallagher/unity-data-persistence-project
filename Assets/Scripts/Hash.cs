using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hash
{
    private static long hashBase = 729117166;
    private static long hashAdder = 209521;
    private static long hashMultiplier = 4044;
    private static long hashModule = 4294967296;

    public static long Apply(string text)
    {
        // min length = 32 char
        while (text.Length < 32)
        {
            text = text + " ";
        }

        long hash = hashBase;
        foreach (char c in text)
        {
            hash = ((int)c * hashMultiplier + hashAdder) % hashModule;
        }

        return hash;
    }
}
