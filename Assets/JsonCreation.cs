using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class JsonCreation : MonoBehaviour
{
    public Text age;
    public Text hobby;
    public Text role;
    public string jsonURL;
    public Jsonclass jsnData;

    void Start()
    {
        StartCoroutine(getData());
    }

    IEnumerator getData()
    {
        Debug.Log("Загрузка...");
        var uwr = new UnityWebRequest(jsonURL);
        uwr.method = UnityWebRequest.kHttpVerbGET;
        var resultFile = Path.Combine(Application.persistentDataPath, "result.json");
        var dh = new DownloadHandlerFile(resultFile);
        dh.removeFileOnAbort = true;
        uwr.downloadHandler = dh;
        yield return uwr.SendWebRequest();
        Debug.Log("Файл сохранён по пути:" + resultFile);
        jsnData = JsonUtility.FromJson<Jsonclass>(File.ReadAllText(Application.persistentDataPath + "/result.json"));
        age.text = jsnData.Age.ToString();
        hobby.text = jsnData.Hobby.ToString();
        role.text = jsnData.Role.ToString();
        yield return StartCoroutine(getData());
    }
    [System.Serializable]

    public class Jsonclass
    {
        public int Age;
        public string Hobby;
        public string Role;
    }
}
