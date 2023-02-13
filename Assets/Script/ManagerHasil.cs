using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerHasil : MonoBehaviour
{
    public Image hasil;
    public GameObject tampilanMasakan;
    public RawImage videoOutput;
    ObjectStatic objectStatic;
    public GameObject gagalPanel;
    public GameObject[] videoHasil;
    public TextMeshProUGUI text;
    public bool vidDone;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("Potong", "Belum Selesai");

        objectStatic = GameObject.Find("Object Static").GetComponent<ObjectStatic>();

        hasil.sprite = objectStatic.hasil[PlayerPrefs.GetInt("noResep")];

        tampilanMasakan.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(vidDone == false)
        {
            if (objectStatic.timeUp)
            {
                videoHasil[0].SetActive(true);
                videoHasil[1].SetActive(false);
                gagalPanel.SetActive(true);
            }
            else
            {
                videoHasil[1].SetActive(true);
                videoHasil[0].SetActive(false);
            }
        }

        if (GetTouchInput())
        {
            vidDone = true;
            videoOutput.gameObject.SetActive(false);
            tampilanMasakan.SetActive(true);
            //foreach(GameObject vid in videoHasil)
            //{
            //    if (vid.activeInHierarchy)
            //    {
            //        vid.SetActive(false);
            //        text.gameObject.SetActive(false);
            //    }
            //}
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

    public void BacktoMenu()
    {
        SceneManager.LoadScene("PilihResep");
    }
}
