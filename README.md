# Genesis Constructa

## Table of contents :page_with_curl:
* [About :information_source:](#about-information_source)
* [Play the game :video_game:](#play-the-game-video_game)
* [How does it look? :eyes:](#how-does-it-look-eyes)
* [Using my assets/entire project :memo:](#using-my-assetsentire-project-memo)
* [Future :crystal_ball:](#future-crystal_ball)

## About :information_source:

This is a pet project that I wanted to do to learn more about making games in Unity for mobile platforms, specifically Android.

It is a 3D idle arcade mobile game. You are playing as a robot sent to a distant planet to build the ground for future colonists. Main mechanic and some other features are based on a game called [Moon Pioneer](https://play.google.com/store/apps/details?id=com.norwichsidegames.tothemoon).

If you want to download the project and do something with it - please, read [Using my assets/entire project :memo:](#using-my-assetsentire-project-memo) section, especially the point about [Shapes](https://assetstore.unity.com/packages/tools/particles-effects/shapes-173167).

## Play the game :video_game:

The game is in review right now, I will link it as soon as it will be published.

## How does it look? :eyes:

Here are some screenshots:

<img src=Screenshots/Genesis_Constructa_screenshot_0.jpg width=200> &nbsp;
<img src=Screenshots/Genesis_Constructa_screenshot_1.jpg width=200> &nbsp;
<img src=Screenshots/Genesis_Constructa_screenshot_2.jpg width=200> &nbsp;
<img src=Screenshots/Genesis_Constructa_screenshot_3.jpg width=200> &nbsp;
<img src=Screenshots/Genesis_Constructa_screenshot_4.jpg width=200> &nbsp;
<img src=Screenshots/Genesis_Constructa_screenshot_5.jpg width=200>

You can also watch a short [gameplay video](https://youtu.be/bGs33GlWUW8)!

## Using my assets/entire project :memo:

- The code and assets in this project are, for the most part, done by me, except for those:
    * Everything in Plugins folder (except for Icons Creator)
    * Rocks and Stalagmites models
    * Water Shader
    * Audio
- Some assets and code depend on [Shapes](https://assetstore.unity.com/packages/tools/particles-effects/shapes-173167), and you will run into problems using those assets, because Shapes Scripts and Shaders folders are not included in the repository since Shapes license prohibits exposing its code. If you have Shapes, you can simply [import](https://docs.unity3d.com/Manual/upm-ui-import.html) missing Scripts and Shaders folders.
- If resources from this repo had some considerable positive impact on your project, please mention me somehow

## Future :crystal_ball:

While I consider this game finished and probably won't work on it anymore, there are still some things I would like to do to make the game "perfect":

- **Add bloom/glow and lighing** - from the beggining of the project I was thinking that I will add nice glow post-processing effect and additional lights to some elements, however, my dreams were shattered by awful performance with those features. But I still want to have bloom and lights! Those effects can be faked, it will probably look nice, but will come with a cost of higher maintenance.
- **Improve performance** - right now the game is not running as smooth as I want it to. I want it to run at 60 fps on my Xiaomi Redmi 9, but I get something between 30 and 60, leaning towards 30 mostly. Most of the performance problems come from rendering. I have a lot of models with multiple submeshes with different materials, which leads to a lot of draw calls, also the water shader, while being absolutely gorgeous, it requires a lot of resources to render it.
- Some minor improvements...
