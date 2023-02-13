using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DragScript : MonoBehaviour
{
    public bool drag;
    public bool needCut;
    public bool myTurn;
    GameManager gameManager;
    public GameObject kompor;
    public bool readyToCook;
    public AudioSource musikWajan;

    ObjectStatic objectStatic;

    //BoxCollider2D box;
    //RectTransform sizeObj;

    // Start is called before the first frame update
    void Start()
    {
        //box = GetComponent<BoxCollider2D>();
        //sizeObj = GetComponent<RectTransform>();

        objectStatic = GameObject.Find("Object Static").GetComponent<ObjectStatic>();

        if (PlayerPrefs.GetInt("noResep") == 2 && gameObject.name == "PahaAyam")
        {
            needCut = false;
        }

        //box.size = new Vector2(sizeObj.rect.width, sizeObj.rect.height);

        musikWajan = GetComponent<AudioSource>();

        if(GameObject.Find("Manager") != null)
        {
            gameManager = GameObject.Find("Manager").GetComponent<GameManager>();

        }
        //kompor = GameObject.Find("Kompor").gameObject;
        //for(int i=0; i < gameManager.cutandCooks[PlayerPrefs.GetInt("noResep")].cut.Length; i++)
        //{
        //    if(gameObject.name == gameManager.cutandCooks[PlayerPrefs.GetInt("noResep")].cut[i])
        //    {
        //        needCut = true;
        //    }
        //}


    }

    // Update is called once per frame
    void Update()
    {
        //if (gameManager.cutDone)
        //{
        //    drag = true;
        //    StartCoroutine(LerpPosition(kompor.transform.position, 3));
        //}
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

    private void OnMouseDrag()
    {
        if (drag && myTurn)
        {
            Vector3 pos_Mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
            transform.position = new Vector3(pos_Mouse.x, pos_Mouse.y, transform.position.z);
        }
        
        //transform.localScale = new Vector2(0.4f, 0.4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Wajan")
        {
            musikWajan.Play();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Moved)
            {
                Debug.Log("TEST SLIDE");
            }
        }

    }

    private void OnMouseDown()
    {
        if(drag == false && needCut && myTurn)
        {
            GameObject.Find("Object Static").GetComponent<ObjectStatic>().bahanMauPotong = gameObject.name;
            SceneManager.LoadScene("PotongScene");
        }

        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "MasakScene")
        {
            gameObject.GetComponent<Image>().maskable = false;
        }
    }

    private void OnMouseUp()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "MasakScene")
        {
            gameObject.GetComponent<Image>().maskable = true;
        }
    }
}