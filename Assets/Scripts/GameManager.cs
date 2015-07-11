using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public int MaxScore;

    public static GameManager GetInstance()
    {
        return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public List<Player> Players;
    Dictionary<CharacterType, PlayableCharacter> PlayersObjects;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("GameController").Length == 2)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(transform.gameObject);
    }

    void Start()
    {
        InitPlayerObjects();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        foreach (var item in PlayersObjects)
        {
            if (item.Value._score >= MaxScore)
                EndGame(item.Value);
        }
    }

    public void ConfigureControls(List<Player> players)
    {
        Players = players;
    }

    void OnLevelWasLoaded(int level)
    {
        InitializeSceneControls();
        InitPlayerObjects();
    }

    void InitPlayerObjects()
    {
        if (PlayersObjects == null || PlayersObjects.Count == 0 || PlayersObjects.ContainsValue(null))
        {
            PlayersObjects = new Dictionary<CharacterType, PlayableCharacter>();
            foreach (var player in new CharacterType[] { CharacterType.Bear, CharacterType.Eagle, CharacterType.Pig })
            {
                var go = GameObject.FindGameObjectWithTag(player.ToString());
                if (go != null)
                    PlayersObjects.Add(player, go.GetComponent<PlayableCharacter>());
            }
        }
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
        GUI.color = Color.black;
        if (_endGame)
        {
            GUI.Label(new Rect(0, 0, 300, 200), new GUIContent("Match is over. " + _winnerName + " has won."), new GUIStyle() { fontSize = 20 });
        }
        else
        {
            int i = 0;
            foreach (var player in PlayersObjects)
            {
                var character = player.Value;
                GUI.Label(new Rect(0, 50 * i, 300, 200), new GUIContent(character.gameObject.name + " : " + character._score), new GUIStyle() { fontSize = 20 });
                i++;
            }
        }
    }

    
}
