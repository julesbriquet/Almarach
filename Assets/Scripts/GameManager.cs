using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<Player> Players;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("GameController").Length == 2)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(transform.gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Application.LoadLevel(Application.loadedLevel);
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
        if (Players != null)
        {
            foreach (var player in Players)
            {

                GameObject.FindGameObjectWithTag(player.characterType.ToString())
                    .GetComponent<PlayableCharacter>().controls = player.controls;
            }
        }
    }
}
