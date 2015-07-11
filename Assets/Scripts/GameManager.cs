using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<Player> Players;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void ConfigureControls(List<Player> players)
    {
        Players = players;
    }

    void OnLevelWasLoaded(int level)
    {
        InitializeSceneControls();
    }

    void InitializeSceneControls()
    {
        foreach (var player in Players)
        {
            GameObject.FindGameObjectWithTag(player.characterType.ToString())
                .GetComponent<PlayableCharacter>().controls = player.controls;
        }
    }
}
