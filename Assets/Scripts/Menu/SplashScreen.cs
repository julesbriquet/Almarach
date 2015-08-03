using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    public RectTransform splashScreen;
    public RectTransform creditsScreen;
    public RectTransform instructionsScreen;
    public RectTransform selectControlsScreen;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && selectControlsScreen.gameObject.activeSelf)
        {
            splashScreen.gameObject.SetActive(true);
            creditsScreen.gameObject.SetActive(false);
            instructionsScreen.gameObject.SetActive(false);
            selectControlsScreen.gameObject.SetActive(false);
            selectControlsScreen.gameObject.GetComponentInChildren<ControlsSelection>().ResetPlayersChoices();
        }
        else if (Input.anyKeyDown && splashScreen.gameObject.activeSelf)
        {
            splashScreen.gameObject.SetActive(false);
            instructionsScreen.gameObject.SetActive(false);
            creditsScreen.gameObject.SetActive(true);
            selectControlsScreen.gameObject.SetActive(false);
        }
        else if (Input.anyKeyDown && creditsScreen.gameObject.activeSelf)
        {
            splashScreen.gameObject.SetActive(false);
            instructionsScreen.gameObject.SetActive(true);
            creditsScreen.gameObject.SetActive(false);
            selectControlsScreen.gameObject.SetActive(false);
        }
        else if (Input.anyKeyDown && instructionsScreen.gameObject.activeSelf)
        {
            StartCoroutine(GoToSelection());
        }
    }

    IEnumerator GoToSelection()
    {
        yield return new WaitForSeconds(1);
        creditsScreen.gameObject.SetActive(false);
        splashScreen.gameObject.SetActive(false);
        instructionsScreen.gameObject.SetActive(false);
        selectControlsScreen.gameObject.SetActive(true);
    }
}
