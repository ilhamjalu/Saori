using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SkipAnimScript : MonoBehaviour
{
    public TextScript todo;
    ObjectStatic objectStatic;

    public double time;
    public double currentTime;
    public GameObject target;
    Scene scene;

    // Use this for initialization
    void Start()
    {
        todo = GameObject.Find("CanvasOverlay").GetComponent<TextScript>();
        objectStatic = GameObject.Find("Object Static").GetComponent<ObjectStatic>();

        scene = SceneManager.GetActiveScene();
        //if(scene.name == "KulkasScene")
        //{
        //    //todo.TampilCentang(todo.checkDone += 1);
        //    //todo.checkDone += 1;

        //    time = target.GetComponent<VideoPlayer>().clip.length - 5;
        //}
        if (scene.name == "BlenderScene")
        {
            time = target.GetComponent<VideoPlayer>().clip.length - 2;
        }
        else
        {
            time = target.GetComponent<VideoPlayer>().clip.length;
        }

        todo.TampilCentang(todo.checkDone += 1);

    }

    void Update()
    {
        if (GetTouchInput() && PlayerPrefs.GetInt("noResep") == 2)
        {
            if(scene.name == "BlenderScene")
            {
                SceneManager.LoadScene("UngkepScene");
            }
            else
            {
                SceneManager.LoadScene("MasakScene");
            }
        }
        else if (GetTouchInput() && PlayerPrefs.GetInt("noResep") == 11)
        {
            if (scene.name == "SausPedasScene")
            {
                SceneManager.LoadScene("KulkasScene");
            }
            else
            {
                SceneManager.LoadScene("MasakScene");
            }
        }
        else if (GetTouchInput() && PlayerPrefs.GetInt("noResep") != 2)
        {
            SceneManager.LoadScene("MasakScene");
        }
        

        currentTime = target.GetComponent<VideoPlayer>().time;
        if ((currentTime + 0.2) >= time)
        {
            if (scene.name == "KulkasScene" || scene.name == "TepungGurameScene" || scene.name == "UngkepScene" || scene.name == "GulungScene")
            {
                SceneManager.LoadScene("MasakScene");
            }
            else if (scene.name == "BlenderScene")
            {
                SceneManager.LoadScene("UngkepScene");
            }
            else if (scene.name == "SausPedasScene")
            {
                SceneManager.LoadScene("KulkasScene");
            }
            else
            {
                SceneManager.LoadScene("HasilScene");
            }
        }
    }

    bool GetTouchInput()
    {
        if (Input.touches.Length > 0)
        {
            Touch touch = Input.GetTouch(0);

            //if (touch.phase == TouchPhase.Began)
            //{
            //    firstPos = new Vector3(touch.position.x, touch.position.y);
            //}

            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("TAP DONE");
                return true;
            }
        }
        return false;
    }


}
