using UnityEngine;
using TMPro;
public class difficulty : MonoBehaviour
{
    public GameObject sceneController;

    public TMP_Text diff;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void left()
    {
        sceneController.GetComponent<SceneChange>().left();
        int scene = sceneController.GetComponent<SceneChange>().toScene;
        
        if(scene == 1) {
            diff.SetText("Easy");
        }else if (scene == 2) {
            diff.SetText("Medium");
        }else
        {
            diff.SetText("Hard");
        }
    }
    
    public void right()
    {
        sceneController.GetComponent<SceneChange>().right();
        int scene = sceneController.GetComponent<SceneChange>().toScene;
        
        if(scene == 1) {
            diff.SetText("Easy");
        }else if (scene == 2) {
            diff.SetText("Medium");
        }else {
            diff.SetText("Hard");
        }
    }
}
