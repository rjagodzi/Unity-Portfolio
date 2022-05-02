using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] Image imageToFade;

    [SerializeField] Image weaponImage;
    [SerializeField] Text weaponName;

    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    [SerializeField] GameObject deathScreen;

    [SerializeField] TextMeshProUGUI coinText;

    [SerializeField] GameObject pauseMenu;

    private void Awake()
    {
        instance = this;  
    }

    public void Fading()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("StartFade");
    }

    public void ChangeWeaponUI (Sprite gunImage, string gunText)
    {
        weaponImage.sprite = gunImage;
        weaponName.text = gunText;
    }

    public void UpdateCoinText(int coinAmount)
    {
        coinText.text = coinAmount.ToString();
    }

    public void DisplayDeathScreen()
    {
        deathScreen.SetActive(true);
        AudioManager.instance.PlaySFX(5);
        AudioManager.instance.PlayDeathMusic();
    }

    public void ReturnToMainMenu()
    {
        LevelManager.instance.ReturnToMainMenu();
    }

    public void RestartLevel()
    {
        LevelManager.instance.RestartLevel();
    }

    public void TurnPauseMenuOnOff(bool onOff)
    {
        pauseMenu.SetActive(onOff);
    }

}
