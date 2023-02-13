using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionScript : MonoBehaviour
{
    public int countCut;
    public bool cutFull;
    public bool potong;
    public int testStep;
    public int[] jumlahPotong;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();

    }

    private void Update()
    {
        if(countCut == gameManager.cutandCooks[PlayerPrefs.GetInt("noResep")].cut.Length && PlayerPrefs.GetString("Potong") != "Selesai")
        {
            cutFull = true;
            if (cutFull)
            {
                SceneManager.LoadScene("PotongScene");
            }
        }
    }

    private void OnMouseEnter()
    {
        Debug.Log("MOUSE ENTER");
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        testStep++;
        Debug.Log(gameManager.cutandCooks[PlayerPrefs.GetInt("noResep")].cut + "(Clone)");
        
        for(int i = 0; i < gameManager.cutandCooks[PlayerPrefs.GetInt("noResep")].cut.Length; i++)
        {
            if (collision.name == gameManager.cutandCooks[PlayerPrefs.GetInt("noResep")].cut[i].ToString() + "(Clone)" && gameManager.cutDone == false)
            {
                Debug.Log(gameManager.cutandCooks[PlayerPrefs.GetInt("noResep")].cut);
                collision.transform.position = gameObject.transform.position;
                //collision.GetComponent<DragScript>().drag = false;
                countCut++;
                
                break;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if(collision.name == gameManager.cutandCooks[PlayerPrefs.GetInt("noResep")] + "(Clone)")
        //{
        //    countCut++;
        //}
    }

}
