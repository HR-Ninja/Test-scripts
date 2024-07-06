using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int coinCount;

    public Vector3 playerPosition;

    public SerializableDictionary<string, bool> coinsCollected;


    public GameData()
    {
        coinCount = 0;
        playerPosition = new Vector3();
        coinsCollected = new SerializableDictionary<string, bool>();
    }

}

