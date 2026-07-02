# Augmented Rap Collection
### Augmented Reality - Final Project

An Augmented Reality application that uses Italian rap album covers 
as image tracking markers. Point your camera at one of the five 
supported album covers to reveal a floating information card with 
details about the album: release year, genre, key tracks, and a 
curated fun fact.

Supported albums:
- Marracash - Persona
- Tedua - Orange County California
- Ernia - Per Soldi e per Amore
- Salmo - Hellvisback
- Lazza - Re Mida

---

## Repository Contents

- `Assets/` - Unity project assets (scripts, prefabs, data, 
   environment)
- `Packages/` - Unity package dependencies
- `ProjectSettings/` - project configuration
- `VideoSimulation.mp4` - demo recorded in XR Simulation 
   (Unity Editor)
- `VideoAndroid.mp4` - demo recorded on a physical Google 
   Pixel 8a via ARCore
- `AR_Report.pdf` - full project report

---

## How to Run

### Option 1 - XR Simulation (no device required)

1. Open the project in **Unity 6.4** (6000.4.1f1) or later
2. Install the required packages via Package Manager if prompted:
   `AR Foundation`, `ARCore XR Plugin`, `XR Simulation`
3. Open `Assets/Scenes/MainScene.unity`
4. Verify that `LivingRoom_RapCollection` is selected as the 
   active environment under  
   `Window > XR > AR Foundation > XR Environment`
5. Press **Play**
6. Navigate using **right-click + WASD** to move the virtual 
   camera toward the album covers and trigger the cards

### Option 2 - Android Device (ARCore-compatible)

1. Switch the build platform to Android via 
   `File > Build Profiles`
2. Enable ARCore under  
   `Edit > Project Settings > XR Plug-in Management > Android`
3. Connect an ARCore-compatible Android device via USB  
   with **USB Debugging** enabled
4. Press **Build and Run**
5. Grant camera permissions when prompted
6. Point the camera at one of the five physical album covers