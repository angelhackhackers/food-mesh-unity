using System;
using System.Collections.Generic;
using UnityEngine;

public class DishData : MonoBehaviour, IClickable
{
    public GameObject SpotLight;

    public float cost;
    public List<string> ingredients = new List<string>();
    public List<float> ratings = new List<float>();
    public List<string> reviews = new List<string>();

    public void OnClicked()
    {
        SpotLight.transform.parent = transform;
        SpotLight.transform.localPosition = new Vector3(0, 0, 0.125f);
        SpotLight.transform.LookAt(transform);
    }

    internal void OnEndHover()
    {
        //throw new NotImplementedException();
    }

    internal void OnHover()
    {
        //throw new NotImplementedException();
    }
}