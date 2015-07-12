using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    public RectTransform splashScreen;
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
            selectControlsScreen.gameObject.SetActive(false);
            selectControlsScreen.GetComponentInChildren<ControlsSelection>().ResetPlayersChoices();
        }
        else if (Input.anyKeyDown && splashScreen.gameObject.activeSelf)
        {
            StartCoroutine(GoToSelection());
        }
    }

    IEnumerator GoToSelection()
    {
        yield return new WaitForSeconds(1);
        splashScreen.gameObject.SetActive(false);
        selectControlsScreen.gameObject.SetActive(true);
    }
}
