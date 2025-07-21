
# 🧠 Depression Diagnosis – VR Immersive Journey

## 📝 Overview

A virtual reality experience designed to explore early signs of depression through immersive, real-time behavioral observation.

## 🧠 Background & Innovation

This project is inspired by a key question from the clinical Beck Depression Inventory (BDI):  
**"Does the patient respond positively to stimuli in their environment?"**  
Instead of relying on self-reported answers, the system observes how users react to natural stimuli within a dynamic virtual world.  
Do they nurture? Do they help? Are they moved by rewards?

The innovation lies in transforming passive testing into an active, emotional journey — one that reveals truths through action, not words.

---

## 🌲 Scenario 1 – Entering the Forest

You awaken in the heart of a lush, vibrant forest. Towering trees surround you, casting soft shadows over moss-covered stones, wildflowers in bloom, and glistening river rocks.  
A stream flows nearby, feeding into a serene body of water. Birds chirp in the distance, and a waterfall murmurs gently in the background.

A subtle guide appears, inviting you to choose the time of day — morning, afternoon, or evening — each affecting the forest’s mood.  
You enter your name to begin your personal journey. This moment is designed to calm the mind and create emotional openness.

For the next three minutes, you are free to explore with no instructions — just the freedom to observe, feel, and breathe.

---

## 🐅 Scenario 2 – The Tiger and the Frozen Lake

You now stand on a forest path beside a flowing stream. A tiger appears, walking toward a large lake. As it reaches the edge, the lake freezes — trapping it in place.  
The tiger turns to you for help.

If you accept, you must locate and water a nearby withered tree. Only by nurturing life can the ice melt and free the tiger.  
This moment quietly examines your emotional engagement: will you act out of empathy, not obligation?

---

## 🔥 Scenario 3 – The Campfire and the Self

Whether or not you helped the tiger, the journey leads you to a new clearing. In the center is an unlit campfire. A burning torch lies nearby.  
A guitar rests on a log, waiting to be played.

You're asked to light the fire — not for the tiger, but for yourself.  
This final scene explores self-care: will you make the effort to bring warmth into your own space?  
A small act with deep meaning.

---

## 🧠 System Logic & Behavioral Data Tracking

The system silently tracks all player behavior during the experience. All data is stored in a structured JSON file under the player's name.

### Data Points Collected:
- **Inactivity Monitoring:** When the player is idle — how long, and when.
- **Object Interaction:** What the player picked up, how many times, and what they did.
- **Key Zone Engagement:** Entries, dwell time, and revisits to designated zones.
- **Decision Tracking:** Whether the player chose to help the tiger, and if they followed through.
- **Self-Initiated Actions:** Whether the player lit the campfire on their own initiative.

All data is later analyzed using criteria defined by a licensed psychiatrist.  
(*Scoring system currently under development.*)

---

## 📁 Folder Architecture

```plaintext
DepressionDiagnosisVR/
├── Assets/
│   ├── Scripts/
│   │   ├── Scenario1/          # Forest entry logic (time selection, roaming)
│   │   ├── Scenario2/          # Tiger & lake interaction logic
│   │   ├── Scenario3/          # Campfire interaction & ending
│   │   ├── DataTracking/       # PlayerDataManager & performance logger
│   │   └── UI/                 # World-space canvas UIs & menu handling
│   ├── Prefabs/                # Interactive prefabs (flowers, torch, fire)
│   ├── Materials/              # Object/environment materials
│   ├── Scenes/                 # Single scene with all logic
│   ├── Resources/              # Fonts, audio, etc.
│   └── Editor/                 # Unit tests (CI)
├── ProjectSettings/            # Unity settings
├── Packages/                   # Unity dependencies
└── README.md                   # Project documentation


##  🛠️ Tech Stack

### 🎮 Game Engine & XR
- **Unity 2023.2.0f1** – Core engine for development and rendering
- **XR Interaction Toolkit** – Full VR interaction system (hands, UI, raycasting)
- **URP (Universal Render Pipeline)** – Optimized real-time lighting and shading for VR

### 🧠 Scripting & Data
- **C#** – Core programming language for all systems
- **JSON (via System.IO)** – Stores session logs per player (movements, decisions, time tracking)

### 🧪 Testing & Automation
- **Unity Test Framework (Editor Mode)** – Automated PlayMode and Unit Tests under `Assets/Editor`
- **GitHub Actions** – Continuous integration: every push runs PlayMode tests to validate stability

### 🕶️ Hardware & Platform
- **Meta Quest 3** – Running via Oculus Link (PC VR runtime)
- **Oculus XR Plugin** – Hardware integration for movement and input

### 🔤 UI & Input
- **World-space Canvas UI** – For immersive VR menus and interactions
- **NonNative Keyboard** – VR-friendly keyboard system for name entry (used without full MRTK)



## 🚀 Installation & Running the Project

1. Clone the project using git clone https://github.com/your-user-name/your-project-name.git
2. Open the project in **Unity 2023.2.0f1**
3. Open the scene located at:
   Assets/NatureManufacture Assets/Forest Environment Dynamic Nature/Demo Scenes/Forest_Demo_Scene.unity
4. press Play to run
5. To build and install the APK manually, use:
    adb install ProjectBuild.apk