using System;
using System.Collections;
using UnityEngine;

public class Rule
{
    private bool usesEightNeighbours;
    private ulong[] value;
    // TODO: Check if possible to do something like: ulong[] | ulong
    public ulong[] Value { get => value; set => this.value = value; }
    private ulong[] WeakValue { get {
            return new ulong[] { value[0], value[1] & 0xffffffffffffff00 };
        } }

    public byte CellNextState { get => BitConverter.GetBytes(value[1])[0]; }

    public Rule(byte[] cells, bool usesEightNeighbours)
    {
        // TODO: This should check endianess
        this.value = new ulong[] { BitConverter.ToUInt64(cells, 8), BitConverter.ToUInt64(cells, 0) };
        this.usesEightNeighbours = usesEightNeighbours;
    }

    public Rule(ulong[] rule, bool usesEightNeighbours)
    {
        this.value = rule;
        this.usesEightNeighbours = usesEightNeighbours;
    }

    /// <summary>
    /// Only this instance will apply WeakValue. This is to minimize the number
    /// of operations. If you want to check against a weak value, then create a 
    /// new Rule using WeakValue.
    /// 
    /// ```
    /// var weakRule = new Rule(strongRule.WeakValue, true);
    /// ```
    /// </summary>
    /// <param name="rule"></param>
    /// <returns></returns>
    public bool Equals(Rule rule, bool exactMatch = false)
    {
        var value = exactMatch ? this.Value : this.WeakValue;

        if (!this.usesEightNeighbours)
        {
            return value[1] == rule.Value[1];
        }

        return value[0] == rule.Value[0] && value[1] == rule.Value[1];
    }


    /// <summary>
    /// For sorting purposes.
    /// </summary>
    /// <param name="rule"></param>
    /// <returns></returns>
    public bool GreaterThan(Rule rule, bool exactMatch)
    {
        var value = exactMatch ? this.Value : this.WeakValue;
        
        return value[0] > rule.Value[0] || (value[0] == rule.Value[0] && value[1] > rule.Value[1]);
    }

    public void PrintRule()
    {
        var a = BitConverter.GetBytes(value[0]);
        var b = BitConverter.GetBytes(value[1]);

        Debug.Log(this.ToString());
    }

    public override string ToString()
    {
        var a = BitConverter.GetBytes(value[0]);
        var b = BitConverter.GetBytes(value[1]);

        return $"Rule N°: {value[0]:X}, {value[1]:X} \n\n Current state: \n {b[1]} {a[0]} {b[7]} \n {b[2]} {a[1]} {b[6]} \n {b[3]} {b[4]} {b[5]} \n\n Changes to: \n {b[1]} {a[0]} {b[7]} \n {b[2]} {b[0]} {b[6]} \n {b[3]} {b[4]} {b[5]}";
    }
}