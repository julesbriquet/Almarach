using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public GameObject endGameUI;

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
    }

	public void EndGame()
	{
        if (_endGame)
        {
            int bestScore = 0;
            PlayableCharacter winner = PlayersObjects[CharacterType.Eagle];
            bool draw = false;
            foreach (var player in PlayersObjects)
            {
                if (player.Value._score > bestScore)
                {
                    winner = player.Value;
                    bestScore = player.Value._score;
                    draw = false;
                }
                else if (player.Value._score == bestScore)
                {
                    draw = true;
                }
            }

            if (!draw)
                EndGame(winner);
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
        Debug.Log("GAME IS FINISHED");
        _endGame = true;
        _winnerName = winner.gameObject.name;
        StartCoroutine(EndGameScreen(winner));
    }

    IEnumerator EndGameScreen(PlayableCharacter winner)
    {
        Controls.controlsEnabled = false;
        endGameUI.SetActive(true);
        if (winner is Bear)
            endGameUI.transform.Find("EndGameBear").gameObject.SetActive(true);
        else if (winner is Eagle)
            endGameUI.transform.Find("EndGameEagle").gameObject.SetActive(true);
        else
            endGameUI.transform.Find("EndGamePig").gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        Controls.controlsEnabled = true;
        Application.LoadLevel(0);
    }

    void OnGUI()
    {
        GUI.color = Color.black;
        if (_endGame)
        {
            GUI.Label(new Rect(0, 0, 300, 200), new GUIContent("Match is over. " + _winnerName + " has won."), new GUIStyle() { fontSize = 20 });
        }
    }
    
}
