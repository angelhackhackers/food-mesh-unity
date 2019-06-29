using System.Collections.Generic;
using UnityEngine;

public class StorePlaceData : MonoBehaviour
{
    public List<DishData> dishes = new List<DishData>();
    public DishData currentDish = null;

    public void SetCurrentDish(DishData targetDish)
    {
        currentDish = targetDish;
    }
}
