using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroLogo : MonoBehaviour
{
    public MeshRenderer backgroundMesh;
    public GameObject oculusLogo;
    public GameObject introContainer;
    public GameObject introText;
    public GameObject googleMapContainer;
    void Awake()
    {
        introText.transform.localScale = Vector3.zero;
        backgroundMesh.material.color = new Color(1, 1, 1, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        backgroundMesh.material.DOColor(Color.white, 1f).SetDelay(1f);
        oculusLogo.transform.DOLocalRotate(new Vector3(60, 0, 0), 1f).SetDelay(3f);
        transform.DOLocalMoveY(2.6f, 1).SetDelay(4f);
        introText.transform.DOScale(Vector3.one, 0.5f).SetDelay(5.5f);
        introContainer.transform.DOLocalMoveY(50, 3f).SetDelay(9f); ;
        backgroundMesh.material.DOColor(new Color(0.25f, 0.25f, 0.25f, 1), 3).SetDelay(10f).OnComplete(()=> googleMapContainer.transform.DOScale(new Vector3(5,5,7),0.5f));
    }

}
