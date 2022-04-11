using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Handbook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneJump(string sceneName)
    {
        SceneManager.LoadScene(sceneName);//³¡¾°Ìø×ª
    }
}
