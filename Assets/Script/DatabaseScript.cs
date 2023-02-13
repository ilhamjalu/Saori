using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.IO;
using System.Data;
using UnityEngine.SceneManagement;
using TMPro;

public class DatabaseScript : MonoBehaviour
{
    string con, filepath;
    public List<string> bahanMakanan;
    public DataScript[] data;
    public TextMeshProUGUI testText;

    private static GameObject instance;

    // Start is called before the first frame update
    void Start()
    {
        //con = "URI=file:" + Application.dataPath + "/Database/dbSaori.db";
        DontDestroyOnLoad(gameObject);

        if(instance == null)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
        //DatabaseConnection();

        filepath = string.Format("{0}/{1}", Application.persistentDataPath, "dbSaori.db");

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->
            
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "dbSaori.db");  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
            testText.SetText("database baru ada");
        }
        else
        {
            testText.SetText("database ada");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void DatabaseConnection()
    //{
    //    using (IDbConnection koneksi = new SqliteConnection(con))
    //    {
    //        koneksi.Open();

    //        using (IDbCommand cmd = koneksi.CreateCommand())
    //        {
    //            string query = "SELECT * FROM TestResep";
    //            cmd.CommandText = query;
    //            using (IDataReader read = cmd.ExecuteReader())
    //            {
    //                while (read.Read())
    //                {
    //                    int step = read.GetInt32(0);
    //                    string bahan = read.GetString(1);
    //                    string activ = read.GetString(2);
    //                    string tex = read.GetString(3);

    //                    Debug.Log("step : " + " " + step + "bahan : " + " " + bahan + "activ : " + activ + "tex : " + tex);
    //                }
    //                koneksi.Close();
    //                read.Close();
    //            }
    //        }
    //    }
    //}

    public void TestingTableDatabase(string tableName)
    {
        using (IDbConnection koneksi = new SqliteConnection(filepath))
        {
            koneksi.Open();

            using (IDbCommand cmd = koneksi.CreateCommand())
            {
                string query = "SELECT * FROM " + tableName;
                cmd.CommandText = query;
                using (IDataReader read = cmd.ExecuteReader())
                {
                    if (data[0].bahan.Count != 0 || data[0].aksi.Count != 0)
                    {
                        data[0].bahan.Clear();
                        data[0].aksi.Clear();
                    }

                    while (read.Read())
                    {
                        int step = read.GetInt32(0);
                        string bahan = read.GetString(1);
                        string activ = read.GetString(2);
                        string tex = read.GetString(3);
                        
                        data[0].bahan.Add(bahan);
                        data[0].aksi.Add(activ);

                        Debug.Log("step : " + " " + step + "bahan : " + " " + bahan + "activ : " + activ + "tex : " + tex);
                    }
                    koneksi.Close();
                    read.Close();
                }
            }

            //SceneManager.LoadScene("InGame");
        }
    }
}