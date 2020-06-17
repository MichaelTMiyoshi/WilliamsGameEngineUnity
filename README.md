# WilliamsGameEngineUnity
The Williams Game Engine for Miyoshi classes done in Unity.  (After completion of the [Visual Studio version](https://github.com/MichaelTMiyoshi/WilliamsGameEngineVS2019).)

(School Project)

## Overview
This version of the Williams Game Engine does not come with a tutorial.  It is just a framework from which other 2D games can be derived.  For those who are unfamiliar with the Williams Game Engine for Miyoshi's classes, it is a space based 2D shooter.  A space ship that the user controls fires lasers that destroy meteors.  In the Visual Studio version (which has a tutorial), the user gets a game over when three meteors get to the far (left) end of the screen.

## Unity Version
This version of the Williams Game Engine assumes that students or other users will use other resources to learn how to create in Unity.  Like I said, there is no tutorial.  Which is why going through the original version using C++ is a necessary first step.  (It does have a tutorial.)

The controls for the ship are just the arrow keys for movement and space bar for shooting.  However, there are some nice features in Unity that make it so that these are not the only controls.  With little to no effort, shooting is also connected to a joystick fire control and the left mouse button via `Fire1`.  This axis input (find it through the `Project Settings->Input Manager`) is also automatically connected to the `<CTRL>` key or `<COMMAND>` key.  It is quite interesting to see what sort of firepower is exerted when these controls are used.  (Use the mouse button though as using the keyboard keys in combination with the arrow keys does some strange things.)  One of the most interesting things is keeping the ship stationary and firing with the mouse.  The pattern of lasers is unexpected at first.  Then, you realize that collisions are taking place.  (Sorry for the spoiler.)
  
As you use Unity, you will find that you can create objects and write code that you think will work great.  Then, you run it and things do not happen the way you think they ought to.  (Or is that just me?)  Make sure to use drag objects to their proper boxes in properties of other objects.  Make sure to add the proper components to objects, including rigidbodies, colliders, and scripts (code).  Obviously, there are more components you could add to objects.  Just make sure to add all the components you need.

## Continuing the Game
If you completed the Visual Studio version, you know that there are pieces missing to this version of the game.  Namely, the animation for when the meteors are destroyed, the sound associated with the explosion, the scoring and lives, and the game over and restart screen.  This is not an oversight.  I want people to have a starting point, not an ending point.  I suppose that it would have been good to put in the animation piece, but that is a good exercise anyway.  The sprite sheet is there to be divided and conquered with just a little investigation into the Unity world.  By the way, Unity has many resources for learning its product.  I would suggest [Ruby's Adventure](https://learn.unity.com/project/ruby-s-2d-rpg) for a great starting point for learning to make 2D games and other applications using Unity.  If nothing else, you should look at the animation portion of that tutorial.

## Code Structure
I have notes in the comments of my code.  A fair amount of notes.  Part of this is because I write code for my students to read.  Part of it is because I follow my own advice and write comments so that I know what I did when I look at said code a couple years later.  Or even a couple months later.

I started to use the `[SerializeField]` notation in my code rather than just making them public.  This change in notation, attitude, and philosophy came from a discussion with a colleague who has taught me much (thanks MikeMag).  The keyword `public` means something to programmers so using it as a substitute for `[SerializeField]` is just being lazy.  At the very least, it is not communicating your intent well with the programmers who look at your code.  Variables and methods that need to be seen by other classes are designated as public (as they must be).

I have a GameManager singleton in my game.  The code is connected to an empty in the scene.  It holds all the necessary globals and it spawns the meteors.  GameManager.cs holds much of my general comments for the whole project.

I brought a piece of code from another project to the game as well.  Boundaries.cs is attached to the ship and controls whether the ship stays within the boundaries of the camera or wraps around to the other side.  I did not spend much time modifying it from the other project so it does have a boundary problem when not wrapping.  The other project assumes the object(s) has velocity rather than just being moved as the ship is in this project.

## Using Git and GitHub
I would suggest learning how to use the commandline Git on your local machine and having a repository on GitHub.  Or whatever online repository you choose to use.  I am still new to both, but I have some [notes](https://github.com/MichaelTMiyoshi/BoidsSimulation/blob/master/TerminalGit.md) that I refer to often in one of my other Unity repos.

I would suggest making the local repo in a folder where you store your Unity projects.  Then, create your Unity project in that folder.  It makes the project one folder deeper in your directory structure, but it makes it cleaner for both Git and GitHub.  Then, go about syncing with Git from the commandline or commandline tool.  You will be much happier than trying to use the GitHub tool in Unity.  (Unless I just could not figure it out.)  At any rate, I have found that using Git from the commandline is easy and quick.

## Final Note
Remember that this is not a tutorial.  It is however, a jumping off point.  You can make many 2D games or apps using this project as a template.  Yes, you will need to learn much about Unity and C#, but it will be worth it to be able to make cool games and apps that you can have available on many platforms, both desktop and mobile.  Spend time on the [Unity Learning](https://unity.com/learn) site.  They have a lot to offer.
