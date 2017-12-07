using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public void NewGameBtn(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }


	// Use this for initialization

    public void CreditsBtn(string credits)
    {
        SceneManager.LoadScene(credits);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if((SceneManager.GetActiveScene()==SceneManager.GetSceneByName("Bad Ending")|| (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Good Ending"))))
        {
            StartCoroutine(Wait(3.0f));
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Level1");
        }


    }

    IEnumerator Wait(float delayInSecs)
    {
        yield return new WaitForSeconds(delayInSecs);
        SceneManager.LoadScene("Credits");

        yield return 0;
    }
}
