using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseComponent : MonoBehaviour
{

    [SerializeField]
    private GameObject pauseScreen;
    [SerializeField]
    private GameObject pauseButton;
    [SerializeField]
    AudioMixer masterMixer;

    [SerializeField]
    GameObject musicSlider;
    [SerializeField]
    GameObject effectsSlider;

    private void Awake()
    {
        pauseScreen.SetActive(false);
        pauseButton = GameObject.Find("PauseButton");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetPauseButtonActive(bool swtichButton)
    {

    }

    void DelayPauseAppear()
    {
        SetPauseButtonActive(true);
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        pauseButton.SetActive(false);
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        pauseButton.SetActive(true);
    }

    public void Quit()
    {

        GameManager.Instance.GetComponent<ScoreManager>().ResetScore();
        GameManager.Instance.GetComponent<ScenesManager>().BeginGame(0);
        Time.timeScale = 1;
    }

    public void SetMusicSlider()
    {
       
        if (musicSlider.GetComponent<Slider>().value == musicSlider.GetComponent<Slider>().minValue)
        {
            //set the music mix completely quiet when it is at min value
            masterMixer.SetFloat("musicVol", -100);
        }
        else
        {
            masterMixer.SetFloat("musicVol", musicSlider.GetComponent<Slider>().value);
        }
    }

    public void SetEffectsSlider()
    {
        
        if (effectsSlider.GetComponent<Slider>().value == effectsSlider.GetComponent<Slider>().minValue)
        {
            //set the music mix completely quiet when it is at min value
            masterMixer.SetFloat("effectsVol", -100);
        }
        else
        {
            masterMixer.SetFloat("effectsVol", effectsSlider.GetComponent<Slider>().value);
        }
    }
}
