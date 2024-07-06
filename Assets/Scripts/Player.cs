using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour, IDataPersistence
{
    public TextMeshProUGUI coin;
    public int money;

    private void Awake()
    {
        coin.text = "Coin count: " + money.ToString();
    }

    private void Update()
    {
        coin.text = "Coin count: " + money.ToString();
    }

    private void OnEnable()
    {
        Coin.coinCollected += CoinCollected;
    }

    private void OnDisable()
    {
        Coin.coinCollected -= CoinCollected;
    }


    void CoinCollected()
    {
        money++;
    }

    public int GetCoins() { return money; }

    public void LoadData(GameData data)
    {
        this.money = data.coinCount;
    }

    public void SavaData(GameData data)
    {
        data.coinCount = this.money;
    }
}