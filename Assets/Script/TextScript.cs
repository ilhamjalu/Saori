using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public TextMeshProUGUI aksiText;
    public GameObject parentText;
    public Image centang;
    DatabaseScript database;
    public List<string> textList;
    public List<Image> checkList;
    public List<TextMeshProUGUI> textPos;
    public Vector3 savePos;
    public static bool begin;

    public int checkDone;

    private static GameObject instance;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }

        savePos = aksiText.transform.position;

        database = GameObject.Find("DatabaseManager").GetComponent<DatabaseScript>();

        if (!begin)
        {
            LoadText();
            begin = true;
        }
        Debug.Log("TEST START");

    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "HasilScene" || scene.name == "PilihResep")
        {
            Destroy(gameObject);
        }
    }

    public void TampilCentang(int loop)
    {

        for(int j = 0; j < checkList.Count; j++)
        {
            Destroy(checkList[j].gameObject);
        }

        checkList.Clear();

        for(int i = 0; i < loop; i++)
        {
            Image centangTemp = Instantiate(centang, centang.transform.position, Quaternion.identity);
            centangTemp.transform.SetParent(GameObject.Find("ContentCentang").transform);
            checkList.Add(centangTemp);
            checkList[i].transform.position = new Vector3(centangTemp.transform.position.x, textPos[i].transform.position.y, 0);
            centangTemp.gameObject.SetActive(true);
        }
    }

    public void LoadText()
    {
        for (int i = 0; i < database.data[PlayerPrefs.GetInt("noResep")].aksi.Count; i++)
        {
            textList.Add(database.data[PlayerPrefs.GetInt("noResep")].aksi[i]);
            savePos.y -= 50f;
            Vector3 posTemp = new Vector3(aksiText.transform.position.x, savePos.y, 0);
            Debug.Log(posTemp);
            TextMeshProUGUI textTemp = Instantiate(aksiText, GameObject.Find("ContentOverlay").transform.position, Quaternion.identity);
            textTemp.transform.SetParent(GameObject.Find("ContentOverlay").transform);
            textTemp.transform.localScale = new Vector3(1, 1, 1);
            textPos.Add(textTemp);
            textPos[i].SetText(i + 1 + ". " + textList[i]);
        }
    }
}
