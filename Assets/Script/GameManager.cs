using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public string[] resepMasak;
    public CutandCook[] cutandCooks;
    public Image hasilMakanan;
    public Sprite[] hasilMakananArray;
    Vector3 firstPos, lastPos;
    public GameObject[] objBahan;
    DatabaseScript databaseScript;
    public ActionScript actionScript;
    public TextMeshProUGUI aksiText;
    public GameObject spawnBahan;
    public bool cutDone;
    public GameObject kompor;
    public int swipeCount;
    public bool testRun;
    public List<GameObject> spawnList;
    public Vector3 spawnX;
    public TextMeshProUGUI sisaSwipe;

    public Button buttonKompor;
    public GameObject mulaiMasak;

    public Transform pemotongan;
    public Transform contentParent;

    private static GameObject instance;
    ObjectStatic objectStatic;
    TextScript todo;

    // Start is called before the first frame update
    void Start()
    {
        databaseScript = GameObject.Find("DatabaseManager").GetComponent<DatabaseScript>();
        objectStatic = GameObject.Find("Object Static").GetComponent<ObjectStatic>();
        todo = GameObject.Find("CanvasOverlay").GetComponent<TextScript>();
        spawnX = spawnBahan.transform.position;
        testRun = true;

        todo.TampilCentang(objectStatic.bahanSudahPotong.Count);
        todo.checkDone = objectStatic.bahanSudahPotong.Count;

        if (PlayerPrefs.GetString("Potong") == "Selesai")
        {
            foreach(GameObject bhn in objBahan)
            {
                for(int j = 0; j < objectStatic.bahanSudahPotong.Count; j++)
                {               
                    string a = GameObject.Find("Object Static").GetComponent<ObjectStatic>().bahanSudahPotong[j];
                    
                    Debug.Log("CHECK DONE " + todo.checkDone);

                    if (bhn.name.Contains(a))
                    {
                        GameObject hasilPotongTemp = Instantiate(bhn, pemotongan.position, Quaternion.identity);
                        hasilPotongTemp.transform.SetParent(pemotongan);
                        hasilPotongTemp.name = bhn.name;
                        hasilPotongTemp.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                        //foreach(Sprite ganti in cutandCooks[PlayerPrefs.GetInt("noResep")].cook)
                        foreach(Sprite ganti in objectStatic.hasilPotong)
                        {
                            if (ganti.name.Contains(a))
                            {
                                hasilPotongTemp.GetComponent<Image>().sprite = ganti;
                                hasilPotongTemp.GetComponent<DragScript>().needCut = false;
                                //objectStatic.stepCount = 0;
                                //objectStatic.stepCount = pemotongan.transform.childCount;
                                //GameObject.Find("Object Static").GetComponent<ObjectStatic>().stepCount += pemotongan.transform.childCount - 1;
                                //Debug.Log("JUMLAH ANAK " + (pemotongan.transform.childCount - 1));
                            }
                        }

                        //Destroy(GameObject.Find(gameObject.name.Contains(a).ToString()));

                    }
                }
            }

            if(objectStatic.bahanSudahPotong.Count == objectStatic.cutandCooks[PlayerPrefs.GetInt("noResep")].cut.Length)
            {
                //buttonKompor.interactable = true;
                mulaiMasak.SetActive(true);
            }

            objectStatic.stepCount += 1;
            //objectStatic.stepCount = pemotongan.transform.childCount;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (actionScript.cutFull)
        {
            bool GetTouchInput()
            {
                if (Input.touches.Length > 0)
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

            if (GetTouchInput())
            {
                sisaSwipe.gameObject.SetActive(true);
                sisaSwipe.text = "Sisa Swipe : " + (4 - swipeCount);

                swipeCount++;
                if (swipeCount > 4)
                {
                    cutDone = true;
                    swipeCount = 0;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || testRun)
        {
            for(int i = 0; i < databaseScript.data[PlayerPrefs.GetInt("noResep")].bahan.Count; i++)
            {
                Debug.Log(databaseScript.data[PlayerPrefs.GetInt("noResep")].bahan.Count);
                for(int j = 0; j < objBahan.Length; j++)
                {
                    Debug.Log(objBahan.Length);
                    if(objBahan[j].name == databaseScript.data[PlayerPrefs.GetInt("noResep")].bahan[i].ToString()) 
                    {
                        //spawnX.x += 2;
                        //Vector3 spawnTemp = new Vector3(spawnX.x, spawnBahan.transform.position.y, 0);
                        //Vector3 spawnTemp = new Vector3(spawnBahan.transform.position.x, spawnBahan.transform.position.y, 0);
                        bool spawn = true;

                        for(int a = 0; a < objectStatic.bahanSudahPotong.Count; a++)
                        {
                            if(objBahan[j].name == objectStatic.bahanSudahPotong[a])
                            {
                                spawn = false;
                                break;
                            }
                            else
                            {
                                spawn = true;
                            }
                        }
                        //GameObject bahanTemp = Instantiate(objBahan[j], spawnBahan.transform.position, Quaternion.identity);
                        if (spawn)
                        {
                            GameObject bahanTemp = Instantiate(objBahan[j], GameObject.Find("Content").transform.position, Quaternion.identity);
                            bahanTemp.transform.SetParent(GameObject.Find("Content").transform);
                            bahanTemp.name = objBahan[j].name;
                            bahanTemp.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                        }
                    }
                    //else
                    //{
                    //    Debug.Log("BAHAN KOSONG" + databaseScript.data[PlayerPrefs.GetInt("noResep")].bahan[i].ToString() + " " + i + j);
                    //}
                }

                testRun = false;
            }

            //LangkahMasak();
            Hasil();
            //PotonganSelesai();
        }

        FixStep();
    }

    public void FixStep()
    {
        for(int i = 0; i < objectStatic.stepMasak.Count; i++)
        {
            if (objectStatic.stepMasak[i])
            {

                GameObject.Find(objectStatic.cutandCooks[PlayerPrefs.GetInt("noResep")].cut[i]).GetComponent<DragScript>().myTurn = true;

            }
        }
    }

    public void PotonganSelesai()
    {
        if (PlayerPrefs.GetString("Potong") == "Selesai")
        {
            for(int i = 0; i < cutandCooks[PlayerPrefs.GetInt("noResep")].cut.Length; i++)
            {
                GameObject hasilPotong = GameObject.Find(cutandCooks[PlayerPrefs.GetInt("noResep")].cut[i] + ("(Clone)"));
                hasilPotong.transform.position = pemotongan.position;
                hasilPotong.GetComponent<SpriteRenderer>().sprite = cutandCooks[PlayerPrefs.GetInt("noResep")].cook[i];
            }
        }
    }

    public void LangkahMasak()
    {
        for(int i = 0; i < databaseScript.data[PlayerPrefs.GetInt("noResep")].aksi.Count; i++)
        {
            int j = i + 1;
            aksiText.SetText(aksiText.text + j + ". " + databaseScript.data[PlayerPrefs.GetInt("noResep")].aksi[i] + "\n");
        }
    }

    public void SwipeCut()
    {
        if (Input.touches.Length > 0)
        {
            Debug.Log("SWIPE MASUK");

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
                    swipeCount++;
                    //return true;
                }
            }
        }
    }

    public void Hasil()
    {
        hasilMakanan.sprite = hasilMakananArray[PlayerPrefs.GetInt("noResep")];
    }

    public void CutandCook()
    {
        if (cutDone)
        {
            kompor.GetComponent<Image>().color = Color.white;
        }
    }

    public void ButtonBack()
    {
        TextScript.begin = false;
        SceneManager.LoadScene("PilihResep");
        PlayerPrefs.SetString("Potong", "Belum Selesai");
    }

    public void KomporButton()
    {
        for(int i = 0; i < pemotongan.childCount; i++)
        {
            pemotongan.transform.GetChild(i).name.Replace("(Clone)", "").Trim();
            objectStatic.bahanSiapMasak.Add(pemotongan.transform.GetChild(i).name);
        }

        for(int j = 0; j < GameObject.Find("Content").transform.childCount; j++)
        {
            objectStatic.bahanSiapMasak.Add(GameObject.Find("Content").transform.GetChild(j).name);
        }

        if (PlayerPrefs.GetInt("noResep") == 11)
        {
            SceneManager.LoadScene("SausPedasScene");
        }
        else if (PlayerPrefs.GetInt("noResep") == 2)
        {
            SceneManager.LoadScene("BlenderScene");
        }
        else if(PlayerPrefs.GetInt("noResep") == 10)
        {
            SceneManager.LoadScene("TepungGurameScene");
        }
        else if(PlayerPrefs.GetInt("noResep") == 5)
        {
            SceneManager.LoadScene("GulungScene");
        }
        else
        {
            SceneManager.LoadScene("MasakScene");
        }
    }
}
