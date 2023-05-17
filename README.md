# Colonization Mobile Game

> **Warning**  
> The project is WIP:construction:

## About :information_source:

This is a pet project that I wanted to do to learn more about making games in Unity for mobile platforms, specifically Android.

It is a 3D idle arcade mobile game. You are a robot sent to a distant planet to build some infrastructure for future colonists. Main mechanic and some other features are based on a game called [Moon Pioneer](https://play.google.com/store/apps/details?id=com.norwichsidegames.tothemoon).

Since it is a work in progress, it lacks some key features and is not really playable at the moment. If you want to download the project and do something with it - please, read [Using my assets/entire project :memo:](#using-my-assetsentire-project-memo) section, especially the point about [Shapes](https://assetstore.unity.com/packages/tools/particles-effects/shapes-173167).

I made the repository public because I got tired of making this game in private and I want to expose and share it's insides.

Hope you will find something useful in here! :purple_heart:

## Screenshots 🖼️

Some screenshots may be out of date and may not represent the current state of the game. I will try to upload relevant screenshots as soon as something visual changes.

<img src=Screenshots/Colonization%20Mobile%20Game_001.jpg width=200> &nbsp;
<img src=Screenshots/Colonization%20Mobile%20Game_002.jpg width=200> &nbsp;
<img src=Screenshots/Colonization%20Mobile%20Game_003.jpg width=200> &nbsp;
<img src=Screenshots/Colonization%20Mobile%20Game_004.jpg width=200> &nbsp;
<img src=Screenshots/Colonization%20Mobile%20Game_005.jpg width=200> &nbsp;
<img src=Screenshots/Colonization%20Mobile%20Game_006.jpg width=200> &nbsp;
<img src=Screenshots/Colonization%20Mobile%20Game_007.jpg width=200>

## What's inside? 🫀

- [Saving-loading system](Assets/Scripts/SaveLoadSystem) to save and load the entire state of the game
- [Stacking](Assets/Scripts/ItemsPlacement), [stack zones](Assets/Scripts/ItemsPlacementsInteractions), and [factories](Assets/Scripts/ItemsExtraction) mechanics, similar to [Moon Pioneer](https://play.google.com/store/apps/details?id=com.norwichsidegames.tothemoon&hl=en&gl=US) ones
- [System](Assets/Scripts/SetupSystem/StackZones) that simplifies interactions with the stack zones
- [Unlock](Assets/Scripts/UnlockingSystem), [build](Assets/Scripts/BuildSystem), and [upgrade](Assets/Scripts/UpgradingSystem) systems
- Simple [debug tools](Assets/Scripts/DebugTools) that allow devs to do the following actions at runtime:
	- Unlock anything
	- Build anything
	- Upgrade anything
	- Give player any items
	- Clear player's inventory
	- Set time scale
- Some useful [extensions](Assets/Scripts/Utility/Extensions) and [helpers](Assets/Scripts/Utility/Helpers)
- Some [models](Assets/Models)

## Using my assets/entire project :memo:

- The code and assets in this project are, for the most part, done by me, except for those:
    * Everything in Plugins folder (except for Icons Creator)
    * Rocks and Stalagmites models
    * Water Shader  
- Some assets and code depend on [Shapes](https://assetstore.unity.com/packages/tools/particles-effects/shapes-173167), and you will run into problems using those assets, because Shapes Scripts and Shaders folders are not included in the repository since Shapes license prohibits exposing its code. If you have Shapes, you can simply [import](https://docs.unity3d.com/Manual/upm-ui-import.html) missing Scripts and Shaders folders.
- Please don't forget to include my license in your projects.
