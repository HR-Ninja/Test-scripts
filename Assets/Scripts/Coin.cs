using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IDataPersistence
{

    [SerializeField] private string id;

    public static event Action coinCollected;

    private bool collected;

    public void LoadData(GameData data)
    {
        data.coinsCollected.TryGetValue(id, out collected);
        if (collected)
        {
            gameObject.SetActive(false);
        }
        else if (!collected)
        {
            gameObject.SetActive(true);
        }
    }

    public void SavaData(GameData data)
    {
        if (data.coinsCollected.ContainsKey(id))
        {
            data.coinsCollected.Remove(id);
        }
        data.coinsCollected.Add(id, collected);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collected)
        {
            collected = true;
            coinCollected?.Invoke();
            gameObject.SetActive(false);

        }
    }


}
