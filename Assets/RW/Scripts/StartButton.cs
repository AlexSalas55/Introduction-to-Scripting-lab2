using UnityEngine;
using UnityEngine.EventSystems; // Required for event systems interfaces
using UnityEngine.SceneManagement; // Required for scene management

// Inherit from IPointerClickHandler to receive OnPointerClick callbacks
public class StartButton : MonoBehaviour, IPointerClickHandler
{
    // This method is called when the GameObject this script is attached to is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        // Load the scene named "Game". Make sure this name matches your game scene file exactly.
        SceneManager.LoadScene("Game");
    }
}