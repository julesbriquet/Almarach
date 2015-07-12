using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ControlsSelection : MonoBehaviour
{
    List<Player> players;

    public Transform[] playerOverlays;
    public Transform[] disneyAnimalSprites;
    public Transform[] animalSprites;
    public Transform[] playerKeyMappings;
    public Transform[] playerGamepadMappings;

    public AudioClip moveSound;
    public AudioClip badSound;
    public AudioClip bearSound;
    public AudioClip pigSound;
    public AudioClip eagleSound;

    private Dictionary<ControlScheme, bool> _listenDirection;
    private Dictionary<ControlScheme, bool> _listenValidate;

    private AudioSource _audioSource;

    // Use this for initialization
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _listenDirection = new Dictionary<ControlScheme, bool>();
        _listenValidate = new Dictionary<ControlScheme, bool>();
        foreach (var scheme in new ControlScheme[]{ 
            ControlScheme.Gamepad1, ControlScheme.Gamepad2, ControlScheme.Gamepad3,
            ControlScheme.Keyboard5123, ControlScheme.KeyboardOKLM, ControlScheme.KeyboardZQSD})
        {
            _listenDirection.Add(scheme, true);
            _listenValidate.Add(scheme, true);
        }
        ResetPlayersChoices();
    }


    // Update is called once per frame
    void Update()
    {
        if (players.Count == 3 && players.All(p => p.characterType.HasValue))
        {
            // proceed to scene
            StartCoroutine(GoToGame());
        }

        foreach (var scheme in new ControlScheme[]{ 
            ControlScheme.Gamepad1, ControlScheme.Gamepad2, ControlScheme.Gamepad3,
            ControlScheme.Keyboard5123, ControlScheme.KeyboardOKLM, ControlScheme.KeyboardZQSD})
        {
            var player = players.FirstOrDefault(p => p.controls == scheme);
            if (Controls.UsePowerUp(scheme) && _listenValidate[scheme])
            {
                _listenValidate[scheme] = false;
                StartCoroutine(SetValidate(scheme, player));
            }
            else
            {
                if (player != null && !player.characterType.HasValue)
                {
                    var leftRight = Controls.GetDirection(scheme).x;
                    if (leftRight != 0 && _listenDirection[scheme])
                    {
                        _listenDirection[scheme] = false;
                        StartCoroutine(SetDirection(scheme, leftRight));
                    }
                }
            }
        }
    }

    public void ResetPlayersChoices()
    {
        players = new List<Player>();
    }

    IEnumerator GoToGame()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().ConfigureControls(players);
        yield return new WaitForSeconds(2); // finish last player selection sound
        Application.LoadLevel("test-scene");
    }

    IEnumerator SetDirection(ControlScheme scheme, float leftRight)
    {
        // change selected character
        ChooseCharacter(scheme, leftRight);
        yield return new WaitForSeconds(0.2f);
        _listenDirection[scheme] = true;
    }

    IEnumerator SetValidate(ControlScheme scheme, Player player)
    {
        // if a player has this character scheme, validate his character type :
        if (player != null)
        {
            if (!players.Any(p => p.characterType == GetFocusedCharacter(scheme)))
            {
                CharacterType characterType = GetFocusedCharacter(scheme);
                disneyAnimalSprites[GetAnimalIndex(characterType)].gameObject.SetActive(false);
                animalSprites[GetAnimalIndex(characterType)].gameObject.SetActive(true);
                player.characterType = characterType;
                _audioSource.PlayOneShot(GetCharacterSound(characterType));
            }
            else
            {
                _audioSource.PlayOneShot(badSound);
            }
        }
        else
        {
            // checkin a player if necessary
            if (players.Count < 3)
                CheckInPlayer(scheme);
        }
        yield return new WaitForSeconds(0.3f);
        _listenValidate[scheme] = true;
    }

    void CheckInPlayer(ControlScheme scheme)
    {
        var newPlayer = new Player() { id = players.Count + 1, controls = scheme };
        players.Add(newPlayer);

        bool isPad = scheme.ToString().StartsWith("Gamepad");

        // show his key mapping :
        if (isPad)
            playerGamepadMappings[newPlayer.id - 1].gameObject.SetActive(true);
        else
            playerKeyMappings[newPlayer.id - 1].gameObject.SetActive(true);

        playerOverlays[newPlayer.id - 1].gameObject.SetActive(true);
    }

    void ChooseCharacter(ControlScheme scheme, float leftRight)
    {
        var player = players.First(p => p.controls == scheme);
        var rect = playerOverlays[player.id - 1].gameObject.GetComponent<RectTransform>();

        var currentPos = rect.anchorMax;
        if (leftRight < 0)
        {
            //move left
            _audioSource.PlayOneShot(moveSound);
            currentPos.x = currentPos.x == 0f ? 1f : currentPos.x - 0.5f;
        }
        else
        {
            //move right
            _audioSource.PlayOneShot(moveSound);
            currentPos.x = currentPos.x == 1f ? 0f : currentPos.x + 0.5f;
        }
        rect.anchorMax = currentPos;
        rect.anchorMin = currentPos;
    }

    CharacterType GetFocusedCharacter(ControlScheme scheme)
    {
        var player = players.First(p => p.controls == scheme);
        var rect = playerOverlays[player.id - 1].gameObject.GetComponent<RectTransform>();

        if (rect.anchorMax.x == 0)
            return CharacterType.Eagle;
        else if (rect.anchorMax.x == 1)
            return CharacterType.Bear;
        else
            return CharacterType.Pig;
    }

    AudioClip GetCharacterSound(CharacterType characterType)
    {
        if (characterType == CharacterType.Eagle) {
            return eagleSound;
        } else if (characterType == CharacterType.Bear) {
            return bearSound;
        } else {
            return pigSound;
        }
    }

    int GetAnimalIndex(CharacterType type)
    {
        switch (type)
        {
            case CharacterType.Eagle: return 0;
            case CharacterType.Pig: return 1;
            case CharacterType.Bear: return 2;
        }
        return -1;
    }

}
