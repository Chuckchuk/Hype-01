# Hype-01
 Final Project for Cs E-23
 By Axel Fay
 (To Submit 12/12/21)
# UNITY VERSION:
> 2021.2.0f1


[NOTE]: This Project uses NO outside assets or files. All Resources and scripts were made by me.


##############
# Controls:
##############
 [W]          - Move Forward
 [S]          - Move Backward
  #             - Movement Speed is Dependent on State (Idle, Walk, or Run)
 [A]          - Rotate Left
 [D]          - Rotate Right
  #             - Rotation Speed is Dependent on State (Idle, Walk, or Run)
 [Space]      - Jump
  #             - Triggers "Jump State"
 [Shift]      - Run
  #             - Triggers "Run State"
 [Mouse_Left] - Attack


################
# Documentation
################
[Note]: The most time was spent in making a somewhat clean Hierarchical State Machine for Player Movement.

> SCENES
  <Assets/Scenes>
  - StartScene
    # The Start Menu, press Space to play
  - SampleScene
    # The Scene that contains the gameplay
  - GameOver
    # When Dead, it will come to this scene

> Game States
  <Assets/Scripts/Game/States>
  - PlayerManager     </PlayerManager.cs>
    #  - Allows me to instantiate the Player so that I can access the player at any time.
    #    (I could have Used tags, but I wanted to try this out as well)
  - Start Game        </StartGame.cs>
    #  - Handles StartGame Scene
    #  - Also Handles GameOver Scene 

> General State Machine: 
  <Assets/Scripts>
  - StateMachine      </StateMachine.cs>
    #  - Framework for any State Machine  
  - BaseState         </BaseState.cs>
    #  - Framework for the "Base State" for any States

> Player States:
  <Assets/Scripts/Entities/Player/States>
  - All States for the Hierarchical State Machine:
    Overall 
    - Movement State Machine    </PlayerMovementSM.cs>
      #   - Handles All Movement States
      #   - Also Handles most Player Input
      #   - Debug Menu (Off by Default)
      #   [!] Also Handles: [Which should theoretically be in another file]
      #     - Player Game Over 
      #     - Quit Game Button Presses
      #     - Attack Button Presses and Actions (Shooting projectile)
    States:
     Top Level:
      - Grounded      </PlayerGrounded.cs>
      - Jumping       </PlayerJump.cs>
        #   - Has an initial force, Gravity force is added to it until it reaches 0 vertical force
      - Falling       </PlayerFall.cs>
        #   - Happens when !isGrounded and at the end of a Jump
     Second Level:
      - Idle          </PlayerIdle.cs>
        #   - Fastest Rotation Speed when Idle
      - Walk          </PlayerWalk.cs>
        #   - Walking has Acceleration and Deceleration
      - Run           </PlayerRun.cs>
        #   - Running has the slowest Rotation speed 
        #   - You can't run backwards

> Player General:
  <Assets/Scripts/Entities/Player>
  - Player Input      </PlayerInput.cs>
    # Generated File from the NEW Unity Input System
  - Sword Projectile  <Sword_Projectile.cs>
    # Attached to the projectile Prefab     (Assets/Resources/Prefabs/Entities/Player/Sword_Projectile)
    # Shoots the Projectile forward and for a certain distance.

> Enemy General:
  <Assets/Scripts/Entities/Enemy>
  - Enemy Projectile  </Projectile01.cs>
    # Attached to the Projectile Prefab     (Assets/Resources/Prefabs/Entities/Enemies/Projectile01 + SmallRange)
    # Shoots the projectile forward and for a certain distance.
  - Statue Shoot      </Statue_Shoot.cs>
    # Handles the Shooting of the statues
    # They shoot forward when a player is in line of sight and in range.
    # [Note] I came up with a very specific and odd raycasting method but it works for these purposes

> Friendly Mage:
  <Assets/Scripts/Entities/Friendly>
  - Win Text          </YouWinText.cs>
    # SImply displays the win text when you enter the characters Trigger Event

#########
# ASSETS
#########

I made all assets in this project.

> Models
The storage of the assets is a mess because I couldn't figure out how to best store the different version of the models
[I.E.] I needed to store a version that wasn't all grouped together, and then a version that was all stored together and THEN a version that was the .fbx file. So my Model storage is a mess.

> Prefabs
  <Assets/Resources/Prefabs>
  All prefabs are stored in that directory.
  I made a prefab for:
    - Enemies
    - Player
    - Projectiles
    - Bridge 
    - Friendly Mage

[Note] : The models are very basic and each has a different style as I was testing out a lot of different ways of making assets. There are also NO animations which is something I had promised for the Basic outcome. (At least there's the Jump from the Better Outcome).

> Textures
  - I made one texture for the Sword Swing

> Materials
  - There's basic solid color materials for the enemies, water, and Friendly Mage


################
# GOAL OF GAME
################
> Reach the top of the tower
> Attack the Enemies
> Don't get Shot
> Don't fall onto the "Water"


[PERSONAL_NOTE]: I honestly wish that I had been able to do so much more. I got really frustrated and demoralized on this project for some reason. Something that I thought would be so simple kept on posing hurdle after hurdle. I spent 6 hours making the simple cliffside that's under the wizard tower, which is ridiculous considering how simple it looks. It was supposed to be a quick mockup, but because of the way I was making the terrain all one mesh, it was meticulous work, and not pheasible. I love making games, it's what I want to do, but I need to figure out how to be happier with way simpler starts. For example, making the Hierarchical State Machine was absolutely unnecessary for the scope of this project, and took way too much time which could have otherwise been used to make an actually playable and interesting game, instead I focused on some invisible aspect that doesn't even work fantastically, and is unnecessary. I'm certainly going to use this as a learning experience, but I just hope I can work on not getting so frustrated so early in development, and learn to just go for a simple simple simple prototype at the start, and then polish things after.
  On another side, I was also getting frustrated at Unity sometimes with how difficult it can be to implement the seemingly simplest things. It took me a full day to figure out how to get projectiles working from the Enemies, as Quaternions were giving me a hard time (only because my enemy was at a slanted angle). And I came up with my own version of raycasting the enemy's vision, but I have no idea if it's good or not. I also used a hacky way to fix a problem with my player's projectile. It kept on shooting vertically, and modifying the quaternion was a nightmare, as well as it no longer shooting forward when I modified the quaternion. So I ended up hackily just rotating the model instead in Blender, which is NOT the solution that I should have to take, but it's the only one I can even still think of.