using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] Image imageToFade;

    private void Start()
    {
        instance = this;
    }

    public void Fading()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("StartFade");
    }
}
