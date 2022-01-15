using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ExtensionsBasic
{
    #region Dictionary Modification
    /// <summary>
    /// Get Random entry where Value is false
    /// and auto filing Value to true for return KeyPair
    /// </summary>
    /// <returns>Random Key</returns>
    public static Tkey GetRandom<Tkey>(this Dictionary<Tkey, bool> entry)
    {
        if (entry == null || entry.Count == 0)
            throw new System.Exception("GetRandom entry null");

        var temp = entry.Where(x => x.Value == false).Select(x => x).ToDictionary(k => k.Key);
        if (temp.Count >= 1)
        {
            var finalPair =  temp.ElementAt(Random.Range(0, temp.Count));
            entry[finalPair.Key] = false;
            return finalPair.Key;
        }
        throw new System.Exception("GetRandom fit entry not found");
    }
    /// <summary>
    /// Set all bool value in dictionary to false
    /// </summary>
    public static void SetFalseValue<TKey>(this Dictionary<TKey, bool> entry)
    {
        if (entry == null || entry.Count() == 0) 
            throw new System.Exception("SetFalseValue entry null");
        Dictionary<TKey, bool> temp = new Dictionary<TKey, bool>();
        foreach (var item in entry)
        {
            temp.Add(item.Key, false);
        }
        entry = temp;
    }
    #endregion
}
