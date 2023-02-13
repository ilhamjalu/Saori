using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectStatic : MonoBehaviour
{
    public List<GameObject> objBahan;
    public List<CutandCook> cutandCooks;
    public List<Sprite> hasilPotong;
    public List<GameObject> siapMasak;
    public List<Sprite> hasil;
    public List<bool> stepMasak;
    public List<bool> stepWajan;
    public int stepCount;
    public int wajanCount;

    public string bahanMauPotong;
    public List<string> bahanSudahPotong;
    public List<string> bahanSiapMasak;

    public string[] saus;
    public bool timeUp;

    GameManager gameManager;


    private static GameObject instance;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
        for(int i = 0; i < gameManager.objBahan.Length; i++)
        {
            objBahan.Add(gameManager.objBahan[i]);
        }

        for(int i = 0; i < gameManager.cutandCooks[PlayerPrefs.GetInt("noResep")].cookObj.Length; i++)
        {
            stepWajan.Add(false);
        }

        for (int i = 0; i < gameManager.cutandCooks.Length; i++)
        {
            cutandCooks.Add(gameManager.cutandCooks[i]);
        }

        for(int i = 0; i < gameManager.cutandCooks[PlayerPrefs.GetInt("noResep")].cut.Length; i++)
        {
            stepMasak.Add(false);
        }

        for (int i = 0; i < gameManager.hasilMakananArray.Length; i++)
        {
            hasil.Add(gameManager.hasilMakananArray[i]);
        }

        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }


    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "PilihResep")
        {
            //bahanSiapMasak.Clear();
            //bahanSudahPotong.Clear();
            Destroy(gameObject);
        }

        if (stepCount > stepMasak.Count - 1)
        {
            stepCount = stepMasak.Count - 1;
        }
        else
        {
            stepMasak[stepCount] = true;
        }

        if (wajanCount > stepWajan.Count - 1)
        {
            wajanCount = stepWajan.Count - 1;
        }
        else
        {
            stepWajan[wajanCount] = true;
        }
        
    }
}
