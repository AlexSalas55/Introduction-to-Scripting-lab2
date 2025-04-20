using UnityEngine;
using UnityEngine.EventSystems; // Required for event systems interfaces

// Inherit from IPointerClickHandler to receive OnPointerClick callbacks
public class QuitButton : MonoBehaviour, IPointerClickHandler
{
    // This method is called when the GameObject this script is attached to is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Quit button clicked. Attempting to quit application...");
        Application.Quit();

        // Add this section if you want it to stop playing in the editor too
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}