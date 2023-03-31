using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverMenu;
	public GameObject pauseMenu;
   
    public AudioSource clickSound;
    public AudioSource music;

    public GameObject loadingScreen;
	public Slider slider;
    public GameObject pauseButton;
    public GameObject[] allObjects;
    public GameObject collisionParticle;
    public GameObject missileCollisionParticle;
    private Spawnner spawner;
    private MovementController player;

    private void Start()
    {
        spawner = FindObjectOfType<Spawnner>();
        player = FindObjectOfType<MovementController>();
       
    }
    public void Play()
	{
        clickSound.Play();       
	    StartCoroutine(PlayAsynchronously());
	}

   
    IEnumerator PlayAsynchronously()
    {
        AsyncOperation operation= SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
        
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress=Mathf.Clamp01(operation.progress / 0.9f);
            slider.value=progress;            
            yield return null;
        }
    }
    public void PauseMenu()
	{
        music.enabled=false;
        clickSound.Play();
        pauseMenu.SetActive(true);
		Time.timeScale=0f;
    }
    public void Resume()
	{
        Time.timeScale=1f;
        music.enabled=true;;
        clickSound.Play();		
		pauseMenu.SetActive(false);	
	}
    public void Restart()
	{
        Time.timeScale=1f;
        clickSound.Play();
        ScoreHandler.score = 0;
        ScoreHandler.missileCount = 0;
        ScoreHandler.missileBonus = 0;
        StartCoroutine(RestartAsynchronously());
    }
    IEnumerator RestartAsynchronously()
    {
        AsyncOperation operation= SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress=Mathf.Clamp01(operation.progress / 0.9f);
            slider.value=progress;
            yield return null;
        }
    }
    public void GameOver()
	{      
        music.enabled=false;
        collisionParticle.transform.position= player.gameObject.transform.position;
        collisionParticle.SetActive(true);
        player.gameObject.SetActive(false);
        StartCoroutine(DisableAllObjects());
	}
   
    public IEnumerator DisableAllObjects()
    {
        yield return new WaitForSeconds(2.5f);
        collisionParticle.SetActive(false);
        spawner.isSpawn = false;
        player.MoveJoyStick.gameObject.SetActive(false);
        pauseButton.SetActive(false);
        Transform[] allChildren = spawner.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            child.gameObject.SetActive(false);
        }
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Menu()
	{	
        clickSound.Play();	
		Time.timeScale=1f;	
        StartCoroutine(MenuAsynchronously());
	}
    IEnumerator MenuAsynchronously()
    {
        AsyncOperation operation= SceneManager.LoadSceneAsync(0);
        
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress=Mathf.Clamp01(operation.progress / 0.9f);
            slider.value=progress;
            yield return null;
        }
    }
   public IEnumerator MissileEffect()
    {
        missileCollisionParticle.SetActive(true);
        yield return new WaitForSeconds(2f);
        missileCollisionParticle.SetActive(false);
    }
    public void Quit()
	{
        clickSound.Play();
		Time.timeScale=1f;
		Application.Quit();
	}
    
}
