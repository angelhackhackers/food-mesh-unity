using System.Collections;
using System.Collections.Generic;
using UniGLTF;
using UnityEngine;
using UnityEngine.Networking;

public class testCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(GetText("output"));
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(StartModel());
        }

    }

    IEnumerator GetText(string file_name)
    {
        string url = "https://mesh-restaurant.s3-ap-northeast-1.amazonaws.com/test-upload/output.glb";
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string savePath = string.Format("{0}/{1}.glb", Application.persistentDataPath, file_name);
                System.IO.File.WriteAllBytes(savePath, www.downloadHandler.data);
                new ImporterContext().Load(savePath);
            }
        }
    }

    IEnumerator StartModel()
    {
        new ImporterContext().Load("untitled.glb");
        yield return null;
    }
}
