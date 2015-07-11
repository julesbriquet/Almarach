using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager GetInstance()
    {
        return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

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
        _endGame = false;
    }

    private bool _endGame;
    private string _winnerName;
    public void EndGame(PlayableCharacter winner)
    {
        _endGame = true;
        _winnerName = winner.gameObject.name;
    }

    void OnGUI()
    {
        if (_endGame)
        {
            GUI.color = Color.black;
            GUI.Label(new Rect(0, 0, 300, 200), new GUIContent("Match is over. " + _winnerName + " has won."), new GUIStyle() { fontSize = 20 });
        }
    }
}
