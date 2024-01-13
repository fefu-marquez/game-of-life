using UnityEngine;
using System.Collections.Generic;

public class JohnConwaysGameOfLife
{
    public static Dictionary<string, Rule> InitializeRules()
    {
        var rules = new Dictionary<string, Rule>();

        for (int i = 0; i < 512; i++)
        {
            var bytes = ExpandBits(i);
            var rule = new Rule(ExpandBits(i), true);
            if (bytes[0] != bytes[9]) rules.Add(rule.Key, rule);
        }

        return rules;
    }

    public static byte[] ExpandBits(int value)
    {
        byte[] result = new byte[16];

        int neighbours = 0;

        for (int i = 0; i < 9; i ++)
        {
            result[9-i] = (value & (1 << i)) != 0 ? (byte) 1 : (byte) 0;

            if (i > 0) neighbours += result[9-i];
        }

        if (result[9] == 0)
        {
            // Cell is born
            if (neighbours == 3) result[0] = 1;
        } else
        {
            // Cell dies by default, so we chek if it's still alive
            if (neighbours == 2 || neighbours == 3) result[0] =  1;
        }

        return result;
    }
}