using System.Collections.Generic;
using System;

public class Ruleset: List<Rule>
{
    bool usesEightNeighbours = true;
    public new void Sort()
    {
        this.Sort((r1, r2) => {
            if (r2.Value[0] == r1.Value[0])
            {
                if (usesEightNeighbours)
                {
                    if (r2.Value[1] > r1.Value[1])
                    {
                        return -1;
                    }

                    if (r2.Value[1] < r1.Value[1])
                    {
                        return 1;
                    }
                }

                return 0;
            }

            if (r2.Value[0] > r1.Value[0])
            {
                return -1;
            }

            return 1;
        });
    }

    public int Find(Rule rule, bool exactMatch = false)
    {
        int min = 0;
        int max = this.Count - 1;

        while (min <= max)
        {
            int mid = (min + max) / 2;
            //this[mid].PrintRule();
            if (this[mid].Equals(rule, exactMatch))
            {
                return mid;
            }

            if (this[mid].GreaterThan(rule, exactMatch))
            {
                max = mid - 1;
            }
            else
            {
                min = mid + 1;
            }
        }

        //return -1;
        throw new Exception($"Did not found any matches for rule: \n\n {rule}");
    }
}