using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class DoorScript : MonoBehaviour
{   
    private Animator animator; // Ajout de l'Animator

    void Start()
{
    animator = GetComponent<Animator>();

    if (animator == null)
    {
        Debug.LogError("⚠️ Animator est NULL sur " + gameObject.name);
    }
}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        if (animator != null)
        {
            animator.SetTrigger("OpenDoor"); 
            yield return new WaitForSeconds(1.5f);
        }
        
        SceneManager.LoadScene("Level02"); 
    }

}
