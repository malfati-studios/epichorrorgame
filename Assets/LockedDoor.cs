using UI;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      UIController.instance.ShowMessage("This door is locked from the other side");
    }
  }
}
