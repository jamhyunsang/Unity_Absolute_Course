using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{

    public void OnClickStartBtn(string msg)
    {
        SceneManager.LoadScene("SceneLoader");
       //SceneManager.LoadScene("Level1");
       //SceneManager.LoadScene("Play",LoadSceneMode.Additive);
    }

}
