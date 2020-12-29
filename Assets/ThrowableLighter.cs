using UnityEngine;

public class ThrowableLighter : MonoBehaviour
{
   private void OnCollisionEnter(Collision other)
   {
      if (other.transform.CompareTag("Slendy"))
      {
         other.gameObject.GetComponent<Slendy>().Die();
         Invoke("GameWon", 5f);
      }
   }

   private void GameWon()
   {
      GameManager.instance.WonTheGame();
   }
}
