using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StoveScript : MonoBehaviour
{
    public float time = 5;
    public bool startTimer;
    public bool stopTimer;
    public bool overCook;
    public float delayTime = 3;
    public List<GameObject> bahanMasak;
    public TextMeshPro swipeText;
    GameManager gameManager;
    DatabaseScript databaseScript;

    public int countObject;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
        databaseScript = GameObject.Find("DatabaseManager").GetComponent<DatabaseScript>();
        countObject = gameManager.objBahan.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(bahanMasak.Count >= databaseScript.data[PlayerPrefs.GetInt("noResep")].bahan.Count)
        {
            Debug.Log("MASAK SIAP");
            SceneManager.LoadScene("MasakScene");
        }

        if (startTimer)
        {
            if(time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                time = 0;
                delayTime -= Time.deltaTime;
                if(delayTime < 0)
                {
                    delayTime = 0;
                    overCook = true;
                }
            }
        }
        else
        {
            if (stopTimer)
            {
                Debug.Log("DONE");
                for (int i = 0; i < bahanMasak.Count; i++)
                {
                    bahanMasak[i].gameObject.SetActive(false);
                }
                gameManager.hasilMakanan.gameObject.SetActive(true);
            }
        }

        if (overCook)
        {
            var newColor = new Color(176, 176, 176);

            for (int i = 0; i < bahanMasak.Count; i++)
            {
                bahanMasak[i].gameObject.SetActive(false);
            }

            gameManager.hasilMakanan.gameObject.SetActive(true);
            gameManager.hasilMakanan.GetComponent<SpriteRenderer>().color = newColor;

            //for (int i = 0; i < bahanMasak.Count; i++)
            //{
            //    if(bahanMasak[i].GetComponent<SpriteRenderer>().color != newColor)
            //    {
            //        Debug.Log("MASUK GANTI WARNA " + i);
            //        bahanMasak[i].GetComponent<SpriteRenderer>().color = newColor;
            //    }
            //}
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bahanMasak.Add(collision.gameObject);
        countObject--;
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (overCook)
        //{
        //    collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(176, 176, 176);
        //}
    }

    public void MulaiMasak()
    {
        startTimer = true;
    }

    public void StopMasak()
    {
        startTimer = false;
        stopTimer = true;
    }
}
