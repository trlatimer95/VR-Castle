## Summary
VR-Castle is a virtual reality experience in which players can explore a small keep and surrounding village. Scattered around the keep and village are various items that players can pickup and interact with.
Certain items also have particular places to be placed such as food on tables, weapons in weapon racks, and logs in a fireplace!

## Build Details
This project was built using Unity 2021.3.16f with XR Interaction Toolkit version 2.3.0-pre.1. 
The project is setup to be built for Oculus (Windows and Android) or OpenXR. Developers just need to change change the build platform, verify settings, and rebuild the project for the desired platform.

## How to Play
Users have a couple options for playing this game. The game is published on Itch.io and SideQuest but can also be downloaded from Github.

### Playing from source project
* Download the source files from Github
  * Click the "Code" on the VR-Castle Github page
  * Click "Download Zip"
  * Once downloaded, unzip the entire zip file
* Open the project in Unity
  * Open Unity Hub
  * Ensure you have a version of Unity that is 2021.6.16f or higher
  * Click "Open" and browse for the unzipped folder
  * Open the project with the desired version of Unity
  * If opening in later version, you will need to agree to upgrading the project and it may introduce errors
* Play the game
  * Once open in Unity, click File > Build Settings
  * Select the target platform you wish to build for
  * Click "Player Settings" to open the project settings
  * Click "XR Plug-in Management" and select the Device you wish to play on. Follow the instructions below for specific devices.
  * If linked, you can then click the play button and play directly on the device
  
### Building for Oculus Rift
* Open build settings, verify target platform is Windows
* Open project settings
* Click "XR Plug-in Management"
* Click the windows tables
* Select Oculus
* Open build settings, click "Build" or "Build and Run"

### Building for Oculus Quest
* Open build settings, verify target platform is Android
* Open project settings
* Click "XR Plug-in Management"
* Click the Android tab
* Select Oculus
* Open build settings, click "Build"
* Load the .apk file into a system that will host the game for your quest (Such as SideQuest)

## Version History
### Version 1.1 - 2/7/2023
* Added instructional UI throughout map
* Fixed arrows able to be picked up after fired but not nocked again
* Fixed bow sounds
* Fixed arrows not pointing in direction they are falling
* Fixed fireplace sounds
* Resized logs to be more realistic
* Fixed guillotine blade not retracting
* Added sound to guillotine
* Added colliders to paths
* Fixed player start position

### Version 1.0 - 2/4/2023
* Implemented bow and arrow
* Made weapons velocity tracking instead of instantaneous (may notice some delay in weapon movement)
* Added option to toggle vignette in settings
* Added sounds to more items
* Fixed ore smelting not working

### Version 0.2 - 1/31/2023
* Redesigned village/courtyard with proper scale assets
* Added forge interactions, players can now smelt gold and iron ore into ingots
* Added weapons and weapon racks around map that player can interact with
* Added footsteps and environment sounds
* Reworked shadows and lighting
* Performed various optimizations
* Made teleport ray only show when teleportation enabled, ray is now a UI ray when teleportation is not enabled
* Fixed table top sockets being too high
* Fixed scale of various objects
* Fixed teleportation not re-enabling after being disabled in settings
* Made weapon racks only accept one weapon per sockets
* Fixed fireplace in keep not working
* Modified attach points for various objects
* Fixed bar doors not opening enough to pass through and opening in opposite direction of hand movement
* Fixed chairs not clicking into sockets at tables
* Fixed certain cabinet doors and drawers not working
* Finished building keep and village

### Version 0.1 - 1/9/2023
* Initial prototype build for public use
* Generated initial builds for Oculus Rift, Quest, and OpenXR
* Keep and village environments mostly built out for testing
* Chair, food, cups, and other items available for interaction
* Sockets for chairs and table tops within keep
* Usabe doors on keep
* Player can walk around the scene
* Player can choose between teleportation and smooth locomotion
* Player can choose between snap and smooth turning
