using UnityEngine;
using System.Collections;

public class TriggerDead : MonoBehaviour
{
    public float destroyDelay = 0.5f; // Temps avant destruction
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DestroyPig());
        }
    }

    IEnumerator DestroyPig()
    {
        // Désactive les composants avant de détruire
        if (transform.parent.TryGetComponent(out Animator animator))
        {
            animator.SetTrigger("Death"); // Jouer animation si existe
        }


        yield return new WaitForSeconds(destroyDelay); 
        Destroy(transform.parent.gameObject); 
    }
}
