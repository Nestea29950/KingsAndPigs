using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    

    private void OnMouseDown()
    {
        print("zaeaz");
        SceneManager.LoadScene("Level01");
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
