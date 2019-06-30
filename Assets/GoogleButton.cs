using OVRSimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[Serializable]
public class uRLJson
{

    public string[] urls;
}

public class RestaurantJSONCollection
{
    public RestaurantJSON[] restaurantJSON;
}
[Serializable]
public class RestaurantJSON
{
    public string restaurant;
    public Coordinates coordinates;
    public string envPhotoUrl;
    public string envSoundUrl;
    public Dish[] dish;


    //    [
    //  {
    //  "restaurant" : "Henri Charpentier Ginza Store",
    //  "coordinates":{"latitude":35.673361, "longitude":139.768244},
    //  "envPhoto":"https://farm66.static.flickr.com/65535/46905986765_d4f2a401c9_b.jpg",
    //  "envSound":"https://soundeffect-lab.info/sound/environment/mp3/restaurant1.mp3",
    //  "food":[
    //  {
    //  "name": "Ramen",
    //  "cost": "300",
    //  "gltfModel":"https://mesh-restaurant.s3-ap-northeast-1.amazonaws.com/test-upload/output.glb"
    //  }
    //  ]
    //  }
    //]
}
[Serializable]
public class Coordinates
{
    public float latitude;
    public float longitude;
}

[Serializable]
public class Dish
{
    public string name;
    public float cost;
    public string glftModelUrl;
}
public class GoogleButton : MonoBehaviour
{
    private uRLJson uRLJson;

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        StartCoroutine(StartJsonRequest("http://ec2-3-112-206-55.ap-northeast-1.compute.amazonaws.com/"));
    }

    public void OnHover()
    {
        GetComponent<SpriteRenderer>().color = Color.grey;

    }
    public void OnEndHover()
    {
        GetComponent<SpriteRenderer>().color = Color.white;


    }
    public void OnClicked()
    {
        SceneManager.LoadScene(1);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine(StartJsonRequest("http://ec2-3-112-206-55.ap-northeast-1.compute.amazonaws.com/"));
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(RestaurantJsonRequest(uRLJson.urls[1]));
        }
    }

    IEnumerator StartJsonRequest(string targetUrl)
    {
        string url = targetUrl;
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            uRLJson = JsonUtility.FromJson<uRLJson>(www.text);
            foreach (string jsonurl in uRLJson.urls)
            {
                Debug.Log(jsonurl);
            }
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        }
    }
    IEnumerator RestaurantJsonRequest(string targetUrl)
    {
        string url = targetUrl;
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            //RestaurantJSON[] restaurantData = JsonHelper.getJsonArray<RestaurantJSON>(www.text);
            //Debug.Log(restaurantData[0].dish[0].glftModelUrl);
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        }
    }


}


