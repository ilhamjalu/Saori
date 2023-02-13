using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonResep : MonoBehaviour
{
    private void Start()
    {
        TextScript.begin = false;
    }
    public void MovetoIngame(int noResep)
    {
        SceneManager.LoadScene("InGame");
        PlayerPrefs.SetInt("noResep", noResep);
    }
}
