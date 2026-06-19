using UnityEngine;
using Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    [Header("player")]
    public GameObject playerprefab;

    [Header("Enemies")]
    public List<GameObject> enemies;

    [Header("References")]
    public Transform SpawnPoint;

    private GameObject _currentplayer;

    [Header("Animation")]
    public float duration = .2f;
    public float delay = .05f;

    public void Start()
    {
        init();
    }

    public void init()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        _currentplayer = Instantiate(playerprefab);
        _currentplayer.transform.position = SpawnPoint.transform.position;
        _currentplayer.transform.DOScale(1, duration).SetDelay(delay).From(0);
    }
}
