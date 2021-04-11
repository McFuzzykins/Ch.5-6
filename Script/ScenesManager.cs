using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    float gameTimer = 0;
    float[] endLevelTimer = { 30, 30, 45 };
    int currentSceneNumber = 0;
    bool gameEnding = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void GetScene()
    {
        scenes = (Scenes)currentSceneNumber;
    }
    void GameTimer()
    {
        switch (scenes)
        {
            case Scenes.level1:
            case Scenes.level2:
            case Scenes.level3:
            {
                if (gameTimer < endLevelTimer[currentSceneNumber - 3])
                {
                     //if level has not completed
                     gameTimer += Time.deltaTime;
                }
                else
                {
                    //if level is completed
                    if (!gameEnding)
                    {
                        gameEnding = true;
                        if (SceneManager.GetActiveScene().name != "level3")
                        {
                            GameObject.FindGameObjectWithTag("Player").GetComponent
                                <PlayerTransition>().LevelEnds = true;
                        }
                        Invoke("NextLevel", 4);
                    }
                }
            break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSceneNumber !=
            SceneManager.GetActiveScene().buildIndex)
        {
            currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
            GetScene();
        }
        GameTimer();
    }
    Scenes scenes;
    public enum Scenes
    {
        bootUp,
        title,
        shop,
        level1,
        level2,
        level3,
        gameOver
    }
    public void BeginGame(int gameLevel)
    {
        SceneManager.LoadScene("gameLevel");
    }
    public void ResetScene()
    {
        gameTimer = 0;
        SceneManager.LoadScene(GameManager.currentScene);
    }
    void NextLevel()
    {
        gameEnding = false;
        gameTimer = 0;
        SceneManager.LoadScene(GameManager.currentScene + 1);
    }
    public void GameOver()
    {
        SceneManager.LoadScene("gameOver");
        Debug.Log("ENDSCORE: " + GameManager.Instance.GetComponent<ScoreManager>().PlayerScore);
    }
    
}
