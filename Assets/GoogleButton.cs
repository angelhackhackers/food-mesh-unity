using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GoogleButton : MonoBehaviour
{

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
            OnClicked();
        }
    }
}
