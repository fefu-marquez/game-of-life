using UnityEngine;
public class JohnConwaysGameOfLife : Ruleset
{
    public void InitializeRules()
    {
        for (int i = 0; i < 512; i++)
        {
            this.Add(new Rule(ExpandBits(i), true));
        }
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