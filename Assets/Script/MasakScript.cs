using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MasakScript : MonoBehaviour
{
    public Button start, stop;
    public ManagerMasak managerMasak;
    public GameObject saori;
    public ObjectStatic objectStatic;
    public TextScript todo;
    public int jumlahBahan;
    public bool checkOut;
    public Sprite[] bahanAkhirList;
    public GameObject bahanAkhir;
    public Transform wajanPos;
    public ParticleSystem asap;
    public bool aduk;
    public float speed = 0.01f;
    public float timeCount = 0.0f;
    public Animator anim;
    public int jumlahTap;
    public GameObject parentBahan;
    public Button adukButt;
    public Image spatula;

    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.Find("ParentBahan").GetComponent<Animator>();
        objectStatic = GameObject.Find("Object Static").GetComponent<ObjectStatic>();
        todo = GameObject.Find("CanvasOverlay").GetComponent<TextScript>();
        start.interactable = false;
        stop.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        objectStatic.wajanCount = GameObject.Find("ParentBahan").transform.childCount;

        if (aduk)
        {
            //SimpleGesture.OnTap(TapTest);
            adukButt.gameObject.SetActive(true);
            spatula.gameObject.SetActive(true);
        }

        if (objectStatic.bahanSiapMasak.Count == 0 && PlayerPrefs.GetInt("noResep") != 2)
        {
            if (objectStatic.bahanSiapMasak.Count == 0 && PlayerPrefs.GetInt("noResep") == 11)
            {
                aduk = true;
                return;
            }
            //saori.SetActive(true);
            for (int i = 0; i < saori.transform.childCount; i++)
            {
                saori.transform.GetChild(i).gameObject.SetActive(true);
                saori.transform.GetChild(i).SetParent(GameObject.Find("Content").transform);
            }
            //stop.interactable = true;
            
        }
        else if (objectStatic.bahanSiapMasak.Count == 0 && PlayerPrefs.GetInt("noResep") == 2)
        {
            aduk = true;
        }
        else
        {
            return;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name == "Gurame")
        {
            collision.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(250, 200);
        }
        else if(collision.name == "PahaAyam")
        {
            collision.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(100, 75);
        }
        else
        {
            collision.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(100, 100);
        }

        if (collision.name.Contains("Saori"))
        {
            //collision.gameObject.SetActive(false);
            start.interactable = true;

            todo.TampilCentang(todo.checkDone + jumlahBahan);
            //StartCoroutine(DelayDestroy(collision.gameObject));

        }

        if (collision.tag == "BahanBubuk")
        {
            StartCoroutine(DelayHide());

            IEnumerator DelayHide()
            {
                yield return new WaitForSeconds(0.1f);
                //collision.gameObject.SetActive(false);
                var tempColor = collision.GetComponent<Image>().color;
                tempColor.a = 0f;
                collision.GetComponent<Image>().color = tempColor;
            }
        }
    }

    IEnumerator StopParticle()
    {
        yield return new WaitForSeconds(1f);
        asap.Stop();
    }

    public void TapTest()
    {
        Debug.Log("TAP");
        jumlahTap += 1;
        //anim.SetBool("rot", true);
        //stop.interactable = true;
        if (jumlahTap >= 3)
        {
            stop.interactable = true;
            parentBahan.SetActive(false);
            bahanAkhir.gameObject.SetActive(true);
            //bahanAkhir.GetComponent<SpriteRenderer>().sprite = bahanAkhirList[PlayerPrefs.GetInt("noResep")];
        }
    }

    public void CircleTest()
    {
        Debug.Log("CIRCLE");
    }

    private void OnMouseUp()
    {
        if (aduk)
        {
            anim.SetBool("rot", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        managerMasak.isMasak = true;
        //collision.transform.Rotate(0, 0, -130);

        //if (collision.name != "Image")
        //{
        //    collision.transform.Rotate(0, 0, -130);

        //}

        objectStatic.wajanCount += 1;

        asap.Play();
        StartCoroutine(StopParticle());

        collision.transform.position = wajanPos.transform.position;
        collision.GetComponent<DragScript>().drag = false;
        collision.transform.SetParent(GameObject.Find("ParentBahan").transform);
        collision.GetComponent<Image>().rectTransform.localScale = new Vector3(1, 1, 1);
        Debug.Log(collision.name);

        managerMasak.startTimer = true;
        //stop.interactable = true;
        jumlahBahan += 1;

        if (collision.name.Contains("Saori"))
        {
            //stop.interactable = true;
            aduk = true;
            collision.gameObject.SetActive(false);
            //jumlahBahan += 1;

            todo.TampilCentang(todo.checkDone + jumlahBahan);

            GameObject parentTemp = GameObject.Find("Spawn Bahan List");
            for (int i=0; i < parentTemp.transform.childCount; i++)
            {
                parentTemp.transform.GetChild(i).gameObject.SetActive(false);
            }

            GameObject.Find("Petunjuk").GetComponentInChildren<TextMeshProUGUI>().SetText("Klik Tombol Aduk");

            //bahanAkhir.gameObject.SetActive(true);

            //bahanAkhir.GetComponent<SpriteRenderer>().sprite = bahanAkhirList[PlayerPrefs.GetInt("noResep")];
        }
        else
        {
            foreach (Sprite temp in managerMasak.bahanWajanNew)
            {
                if (temp.name.Contains(collision.name))
                {
                    //collision.transform.position = transform.position;
                    collision.GetComponent<Image>().sprite = temp;           

                    if(collision.name == "DagingAyam" && PlayerPrefs.GetInt("noResep") == 11)
                    {
                        for(int i = 0; i< managerMasak.bahanWajanNew.Length; i++)
                        {
                            if(managerMasak.bahanWajanNew[i].name == "DagingAyamBumbu")
                            {
                                collision.GetComponent<Image>().sprite = managerMasak.bahanWajanNew[i];
                            }
                        }
                    }

                    if (collision.name == "Kacang")
                    {
                        collision.GetComponent<Image>().rectTransform.localScale = new Vector3(0.2f,0.2f, 0.2f);
                    }
                    else
                    {
                        collision.GetComponent<Image>().rectTransform.localScale = new Vector3(1, 1, 1);

                    }
                }
                else
                {

                    Debug.Log("GAK GANTI " + temp.name);
                }
            }
        }

        for (int i = 0; i < objectStatic.bahanSiapMasak.Count; i++)
        {
            if (collision.name.Contains(objectStatic.bahanSiapMasak[i]))
            {
                objectStatic.bahanSiapMasak.RemoveAt(i);
                todo.TampilCentang(todo.checkDone + jumlahBahan);
                Debug.Log("KELUAR");
            }
        }



        //StartCoroutine(DelayDestroy(collision.gameObject));
    }


    IEnumerator DelayDestroy(GameObject des)
    {
        yield return new WaitForSeconds(1f);

        if (des.name.Contains("Saori"))
        {
            des.SetActive(false);
            bahanAkhir.gameObject.SetActive(true);
            bahanAkhir.GetComponent<Image>().sprite = bahanAkhirList[PlayerPrefs.GetInt("noResep")];
        }
        else
        {
            foreach(Sprite temp in managerMasak.bahanWajan[PlayerPrefs.GetInt("noResep")].bahanGanti)
            {
                if (des.name.Contains(temp.name))
                {
                    des.transform.position = transform.position;
                    des.GetComponent<Image>().sprite = temp;
                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.Rotate(0, 0, 130);
        //jumlahBahan -= 1;
        //todo.TampilCentang(todo.checkDone -= 1);

    }
}
