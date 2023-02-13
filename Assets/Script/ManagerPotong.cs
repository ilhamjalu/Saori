using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerPotong : MonoBehaviour
{
    //GameManager gameManager;
    ObjectStatic objectStatic;

    public Transform bahanSpawn;
    public Transform potongSpawn;
    public Transform outFrame;
    public Vector3 bahanTempPos;

    Vector3 firstPos, lastPos;
    public bool notEmpty;
    public int swipeCount;
    public int swipeTemp = 0;

    public List<GameObject> bahan;
    public List<bool> kondisiBahan;

    public AudioSource potongSound;

    // Start is called before the first frame update
    void Start()
    {
        //gameManager = GameObject.Find("Manager").GetComponent<GameManager>();

        objectStatic = GameObject.Find("Object Static").GetComponent<ObjectStatic>();
        bahanTempPos = bahanSpawn.position;

        foreach (GameObject bhn in objectStatic.objBahan)
        {
            if (bhn.name.Contains(objectStatic.bahanMauPotong))
            {
                GameObject bhnTemp = Instantiate(bhn, potongSpawn.position, Quaternion.identity);
                bhnTemp.transform.SetParent(potongSpawn);
                bhnTemp.name = bhn.name;
                Destroy(bhnTemp.GetComponent<DragScript>());
                bhnTemp.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                bahan.Add(bhnTemp);
            }
        }

        StartCoroutine(DelaySwipe());
    }

    // Update is called once per frame
    void Update()
    {
        if (notEmpty)
        {
            if (GetTouchInput())
            {
                //Debug.Log("SISA : " + (SetSwipeCount() - swipeCount));
                
                swipeCount++;
                potongSound.Play();
                bahan[0].GetComponent<Image>().sprite = bahan[0].GetComponent<OrderManager>().orderCut[0];
                bahan[0].GetComponent<OrderManager>().orderCut.RemoveAt(0);
            }
        }

        if (bahan[0].GetComponent<OrderManager>().orderCut.Count == 0)
        {
            objectStatic.bahanSudahPotong.Add(bahan[0].name);
            
            PlayerPrefs.SetString("Potong", "Selesai");
            SceneManager.LoadScene("InGame");
        }
    }

    bool GetTouchInput()
    {
        Debug.Log("MASOK LO");
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                firstPos = new Vector3(touch.position.x, touch.position.y);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                lastPos = new Vector3(touch.position.x, touch.position.y);

                if (lastPos != firstPos)
                {
                    Debug.Log("LAST POST : " + lastPos + " FIRST POS : " + firstPos);
                    //swipeCount++;

                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator DelaySwipe()
    {
        yield return new WaitForSeconds(1);
        notEmpty = true;
    }

    IEnumerator DelayHide(int index)
    {
        yield return new WaitForSeconds(1);
        bahan[index].gameObject.SetActive(false);
    }

    int SetSwipeCount()
    {
        for (int i = 0; i <= kondisiBahan.Count; i++)
        {
            Debug.Log("TEST SET");
            if (!kondisiBahan[i])
            {
                if(swipeTemp == 0)
                {
                    swipeTemp = bahan[i].GetComponent<OrderManager>().orderCut.Count;
                    break;
                }
                else
                {
                    return swipeTemp;
                }
                
            }
        }

        return swipeTemp;
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration, GameObject bahanPos)
    {
        float time = 0;
        Vector3 startPosition = bahanPos.transform.position;
        while (time < duration)
        {
            bahanPos.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        bahanPos.transform.position = targetPosition;
        
        //notEmpty = true;
    }

    public void SwipeCut()
    {
        if (Input.touchCount > 0)
        {
            Debug.Log("SWIPE MASUK");

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("BEGAN");
                firstPos = new Vector3(touch.position.x, touch.position.y);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("END");
                lastPos = new Vector3(touch.position.x, touch.position.y);

                if (lastPos != firstPos)
                {
                    Debug.Log("LAST POST : " + lastPos + " FIRST POS : " + firstPos);
                    swipeCount++;
                    //return true;
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        Debug.Log("TEST");
        if (!notEmpty)
        {
            //SwipeCut();
        }

    }

    public void ButtonBack()
    {
        TextScript.begin = false;
        SceneManager.LoadScene("PilihResep");
        PlayerPrefs.SetString("Potong", "Belum Selesai");
    }
}
