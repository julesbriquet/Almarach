using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public static GameManager GetInstance()
    {
        return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    public List<Player> Players;
    public AudioClip bearWinSound;
    public AudioClip eagleWinSound;
    public AudioClip pigWinSound;
    Dictionary<CharacterType, PlayableCharacter> PlayersObjects;
    AudioSource _audioSource;

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
        _audioSource = GetComponent<AudioSource>();
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
        if (!_endGame)
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

    private bool _endGame = false;

    public void EndGame(PlayableCharacter winner)
    {
        _endGame = true;
        StartCoroutine(EndGameScreen(winner));
    }

    IEnumerator EndGameScreen(PlayableCharacter winner)
    {
        Controls.controlsEnabled = false;
        GameObject endGameUI = GameObject.Find("EndScreens");
        endGameUI.GetComponent<Image>().enabled = true;
        Text endText = endGameUI.transform.FindChild("EndText").GetComponent<Text>();
        if (winner is Bear)
        {
            endGameUI.transform.FindChild("EndBear").GetComponent<Image>().enabled = true;
            _audioSource.PlayOneShot(bearWinSound);
            endText.color = new Color(1f, 200f / 255f, 41f / 255f);
            endText.text = "Bear won!";
        }
        else if (winner is Eagle)
        {
            endGameUI.transform.FindChild("EndEagle").GetComponent<Image>().enabled = true;
            _audioSource.PlayOneShot(eagleWinSound);
            endText.color = new Color(0f, 227f / 255f, 20f / 255f);
            endText.text = "Eagle won!";
        }
        else
        {
            endGameUI.transform.FindChild("EndPig").GetComponent<Image>().enabled = true;
            _audioSource.PlayOneShot(pigWinSound);
            endText.color = new Color(182f / 255f, 87f / 255f, 161f / 255f);
            endText.text = "Pig won!";
        }
        endText.enabled = true;
        yield return new WaitForSeconds(4f);
        Controls.controlsEnabled = true;
        Application.LoadLevel(0);
    }
    
}
