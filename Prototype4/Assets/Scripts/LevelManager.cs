using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string sceneName;
    public Selection selection;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void changeScene()
    {
       

        if (!string.IsNullOrEmpty(sceneName))
        {
            Debug.Log(sceneName);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Nome da cena está vazio!");
        }
    }
}
