using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Character
{
    public int id;
    public int count;
    public bool isOn;
}

[Serializable]
public class CharactersDataLoad : ILoader<int, Character>
{

    public List<Character> Characters = new List<Character>();
    
    public Dictionary<int, Character> MakeDict()
    {
     
        Dictionary<int, Character> dic = new Dictionary<int, Character>();

        foreach (Character character in Characters)
        {
            dic.Add(character.id, character);
        }

        return dic;
    }
}
