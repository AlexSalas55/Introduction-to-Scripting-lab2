# Sheep Rescue - Enhanced

This project enhances the original Kodeco "Sheep Rescue" tutorial game.

## Added Features

*   **Main Menu:** A title screen with "Start Game" and "Quit Game" buttons.
*   **Jumping Sheep:** Sheep now jump randomly, making them harder to hit.
*   **Lives Counter:** Players start with 3 lives and lose one for each escaped sheep. Displays "You lose!" at zero lives.
*   **Score Counter:** Players gain points for hitting sheep with hay.

## Relevant Scripts

*   `Sheep.cs` (Jump logic, score/life triggers)
*   `SheepSpawner.cs` (Life/Score management, UI updates)
*   `StartButton.cs` / `QuitButton.cs` (or `MainMenuManager.cs`) (Title screen button logic)
