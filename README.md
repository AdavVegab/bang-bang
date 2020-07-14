# BANG!

VR Workshop - FH Münster - SS2020

## Materials

For this project a Grid Material was necessary, this was made using the Shader Graph

To Apply this material just right click on the XXXXXX and select "New Material"

this can be configured as following:

- **Tiling**: Number of horinzontal and vertical lines
- **Thickness**: The thickness of the Neon Lines
- **Color**: The color (HDR), the intensity can be used to adjust the Bool effect

## Scripts

### Audio Processing

> /Assets/Scripts/AudioProcessing.cs

This Script Processes the Audio Track and with the help of the function

`GetSpectrumData(float[] samples, int channel, FFTWindow window)`

this will return the samples (with 43 Hz per Sample) which then are in turn divided in 8 bands according to the following:

- 0 -> 2 = 86 Hz -> 0 - 87 Hz
- 1 -> 4 = 172 Hz -> 87 - 258 Hz
- 2 -> 8 = 344 Hz -> 259 - 602 Hz
- 3 -> 16 = 688 Hz -> 603 - 1290 Hz
- 4 -> 32 = 1376 Hz -> 1291 - 2666 Hz
- 5 -> 64 = 2752 Hz -> 2667 - 5418 Hz
- 6 -> 128 = 5504 Hz -> 5419 - 10922 Hz
- 7 -> 256 = 11008 Hz -> 10923 - 21930 Hz

the samples are averaged, group, and then stored in the array `_freqBand`

then the script uses a buffer to soften the change in the audio, creating the `_bandBuffer` array.

lastly the script uses the highest recorded value of each band to create the arrays `_audioBand` and `_audioBandBuffer` respectively, which are the normalized versions of the `_freqBand` and `_bandBuffer`

you can use this arrays in other scripts to create interesting interactions with the sound:

`_freqBand` : average of the frequency bands

`_bandBuffer` : buffered version of the `_freqBand`

`_audioBand` : normalized version of the `_freqBand`

`_audioBandBuffer`: normalized version of the `_bandBuffer`

#### Usage

- attach to an AudioSource
- call any of the array in other scripts, a call example can be found on the script AudioReact.cs

### Audio React

> /Assets/Scripts/AudioReact.cs

This script allows the GameObject to change the position in the y axis according to the value of the `_audioBand` or `_audioBandBuffer`, a GameObject with the script Audio Processing has to be present in the scene.

#### Options

**band:** which band from AudioReact should be used for the movement

**maximal position:** position (in the y axis) corresponding to the maximal value of `_audioBand` or `_audioBandBuffer`

**use buffer:** if selected `_audioBandBuffer` will be used instead of `_audioBand` (softer movement)

#### Usage

- Make sure that an Audio Source has the script `AudioProcessing.cs` attached to it
- select the band an maximal position

### Projectile Collision

> /Assets/Scripts/DetectCollision.cs

will try to detect a collision with any of the player hands. when a collision is detected, the GameObject is deleted, and replaced with a explosion, then the materials of the buildings will be changed.

#### Options

**collision radius:** how close does the hand have to be to be detected as collision

**explosion:** Prefab of the explosion

#### Usage

- Mark the GameObjects that represent the player hands with the Tags `Hand1` and `Hand2`
- Mark the Objects where the Material should change with the Tag `Building`
- Add the Script `MaterialBuilding.cs` to the objects with the `Building` Tag
- Add this Script to the Projectile Prefab

### Material Change

> /Assets/Scripts/MaterialBuilding.cs

Tis Scripts allow other Objects to call the `TurnOn()` function, wich changes the material, then after a delay the material is changed back.

#### Options

**time on material:** time that the material on should stay on the object

**material off:** material to assign after the timer (time on material) run off

**material on:** material to assign when the `TurnOn()` function is called

#### Usage

- Mark the GameObject with the tag `Building`
- Add the Script
- Set the Materials (on / off)
- This will only have any effect if other objects are using the script `DetectCollision.cs`

### Spawn Projectiles

> /Assets/Scripts/SpawnProjectile.cs

Will control the creation of projectiles groups at exact times and with specific behaviors

#### Options

**Audio track:** the Audio source of the scene, should only be applied to one spawner

**Projectiles:** Prefab to spawn

**First after:** after how many seconds after the start of the Audio Track should the first projectile be spawned

**Delay between projectiles:** how many time should pass before the next projectile in this groups is spawned

**Start With Left:** will spawn the first projectile of the group on the left if selected (irrelevant when double is selected)

**Double:** all projectiles in this group will spawn as pairs (left and right)

**Distance from the center:** distance (x axis) from the GameObject to spawn the projectiles

**Destroy after:** Lifespan of the projectiles

**Correction:** How much time before (from First After) should the projectile be spawned (recommended = Distance/10)

**Nr of projectiles:** how many projectiles should be spawned in this group

#### Usage

- attach the script to an empty Game Object
- Place the object at the spawn point of the projectiles
- Set the options (audio Track is only needed in one spawner

## Projectiles

### VFX Graph