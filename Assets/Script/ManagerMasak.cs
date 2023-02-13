using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerMasak : MonoBehaviour
{
    public bool mulai;
    public bool startTimer;
    public bool stopTimer;
    public Image bar;
    public GameObject[] sausSaori;
    public BahanSiapMasakHandler[] bahanMasak;
    public BahanWajan[] bahanWajan;
    public Sprite[] bahanWajanNew;
    public Transform wajanPos;
    public Transform spawnList;
    public Vector3 posTemp;
    public AudioSource masakSound;
    public Animator adukAnim;

    public SpecialScript[] specialScripts;
    public int[] specialNum;
    public bool special;
    public int nomer;

    public bool isMasak;
    ObjectStatic objectStatic;
    MasakScript masakScript;

    // Start is called before the first frame update
    void Start()
    {
        masakScript = GameObject.Find("Penggorengan").GetComponent<MasakScript>();
        //PlayerPrefs.SetString("Potong", "BelumSelesai");
        //posTemp = spawnList.transform.position;
        objectStatic = GameObject.Find("Object Static").GetComponent<ObjectStatic>();

        for (int i = 0; i < sausSaori.Length; i++)
        {
            if (sausSaori[i].name == objectStatic.saus[PlayerPrefs.GetInt("noResep")])
            {
                sausSaori[i].GetComponent<DragScript>().drag = true;
                sausSaori[i].GetComponent<DragScript>().myTurn = true;
                sausSaori[i].GetComponent<DragScript>().needCut = false;
            }
        }

        for(int s = 0; s < specialNum.Length; s++)
        {
            if (PlayerPrefs.GetInt("noResep") == specialNum[s])
            {
                nomer = s;
                special = true;
            }
        }

        if(special)
        {
            objectStatic.bahanSiapMasak.Clear();

            for (int y = 0; y < specialScripts[nomer].bahanJadi.Length; y++)
            {
                GameObject bahanTemp = Instantiate(specialScripts[nomer].bahanJadi[y], GameObject.Find("Content").transform.position, Quaternion.identity);
                bahanTemp.name = specialScripts[nomer].bahanJadi[y].name;
                bahanTemp.transform.SetParent(GameObject.Find("Content").transform);
                bahanTemp.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                bahanTemp.GetComponent<DragScript>().drag = true;
                bahanTemp.GetComponent<DragScript>().needCut = false;

                objectStatic.bahanSiapMasak.Add(bahanTemp.name);

                foreach (Sprite ganti in objectStatic.hasilPotong)
                {
                    if (ganti.name.Contains(bahanTemp.name))
                    {
                        bahanTemp.GetComponent<Image>().sprite = ganti;
                        bahanTemp.GetComponent<DragScript>().needCut = false;

                    }
                }
            }

        }
        else
        {
            for (int j = 0; j < objectStatic.objBahan.Count; j++)
            {
                for (int a = 0; a < objectStatic.bahanSiapMasak.Count; a++)
                {
                    if (objectStatic.objBahan[j].name.Contains(objectStatic.bahanSiapMasak[a]))
                    {
                        //posTemp.y -= 1.5f;
                        GameObject bahanTemp = Instantiate(objectStatic.objBahan[j], GameObject.Find("Content").transform.position, Quaternion.identity);
                        bahanTemp.name = objectStatic.objBahan[j].name;
                        bahanTemp.transform.SetParent(GameObject.Find("Content").transform);
                        bahanTemp.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                        bahanTemp.GetComponent<DragScript>().drag = true;
                        bahanTemp.GetComponent<DragScript>().needCut = false;
                        foreach (Sprite ganti in objectStatic.hasilPotong)
                        {
                            if (ganti.name.Contains(bahanTemp.name))
                            {
                                bahanTemp.GetComponent<Image>().sprite = ganti;
                                bahanTemp.GetComponent<DragScript>().needCut = false;

                            }
                        }
                    }
                }

            }
        } 
    }
    
    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            if (isMasak)
            {
                masakSound.Play();
                isMasak = false;
            }
            bar.fillAmount += 0.01f * Time.deltaTime;
            if(bar.fillAmount == 1f)
            {
                objectStatic.timeUp = true;
                StartCoroutine(DelayMoveScene());
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach(GameObject test in GameObject.FindGameObjectsWithTag("Bumbu"))
            {
                test.SetActive(true);
            }
            //GameObject.Find
        }

        FixStep();
    }

    public void FixStep()
    {
        for (int i = 0; i < objectStatic.stepWajan.Count; i++)
        {
            if (objectStatic.stepWajan[i])
            {
                Debug.Log("NAMA BAHAN : " + objectStatic.cutandCooks[PlayerPrefs.GetInt("noResep")].cookObj[i]);
                GameObject.Find(objectStatic.cutandCooks[PlayerPrefs.GetInt("noResep")].cookObj[i]).GetComponent<DragScript>().myTurn = true;

            }
        }
    }

    public void MulaiMasak()
    {
        startTimer = true;
    }

    public void StopMasak()
    {
        startTimer = false;
        stopTimer = true;
        objectStatic.timeUp = false;
        StartCoroutine(DelayMoveScene());
    }

    IEnumerator DelayMoveScene()
    {
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("HasilScene");

        //if (PlayerPrefs.GetInt("noResep") == 2 && !objectStatic.timeUp)
        //{
        //    SceneManager.LoadScene("UngkepScene");
        //}
        //else
        //{
        //    SceneManager.LoadScene("HasilScene");
        //}
    }

    public void AdukButton()
    {
        adukAnim.SetTrigger("triggerAduk");

        StartCoroutine(DelayHasilMasak());
    }

    IEnumerator DelayHasilMasak()
    {
        yield return new WaitForSeconds(3.3f);

        masakScript.stop.interactable = true;
        masakScript.parentBahan.SetActive(false);
        masakScript.bahanAkhir.gameObject.SetActive(true);
        masakScript.bahanAkhir.GetComponent<SpriteRenderer>().sprite = masakScript.bahanAkhirList[PlayerPrefs.GetInt("noResep")];

    }

    public void ButtonBack()
    {
        SceneManager.LoadScene("PilihResep");
        PlayerPrefs.SetString("Potong", "Belum Selesai");
    }
}
