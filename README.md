# Erasers Territory

A top-down 2D asymmetric prototype game built in Unity.

## Overview
Eraser’s Territory is a 2D game prototype developed using Unity, focused on territory control and player interaction mechanics.
This project explores gameplay systems such as movement, interaction, and basic combat logic.

---

## Gameplay Concept
- **Pencil**
  - Draws trails on the map
  - Can switch between multiple trail types:
    - Default (visual only)
    - Circuit (functional logic)
    - Trap (functional logic)
  - Circuit/Trap trails activate a hidden logic grid

- **Eraser**
  - Chases the Pencil
  - Physically erases trail segments
  - Defuses active circuit cells on the grid
  - Breaks circuits even if the visual trail looks connected

- **Victory Condition (WIP)**
  - Pencil wins by completing an unbroken circuit
  - Eraser wins by breaking the circuit before completion

---

## Tech Stack

- **Engine:** Unity (2D)
- **Language:** C#
- **Input:** Unity Input System
- **Rendering:** LineRenderer
- **Architecture F**
- Git \& GitHub for version control

---

## 🧠 Key Systems Implemented

### 1. Trail System
- LineRenderer-based trails
- Trails split into **segments** when erased
- Supports **partial erasing** (holes in trails)
- Trail color and behavior depend on TrailType


### 2. Grid-Based Circuit Logic
- Invisible **logic grid** (not visual-based logic)
- Each cell represents circuit state
- Pencil writes to grid
- Eraser clears grid cells
- Visual trails are cosmetic — grid is the source of truth


### 3. Trail Types
| Trail Type | Visual | Affects Grid |
|-----------|--------|--------------|
| Default   | ✅ Yes | ❌ No |
| Circuit   | ✅ Yes | ✅ Yes |
| Trap      | ✅ Yes | ✅ Yes |


### 4. Eraser Logic
- Cooldown-based erasing
- Movement-gated defusing (no AFK erasing)
- Radius-based interaction
- Grid defuse is independent of visuals


### 5. Debug Visualization
- Gizmo-based grid visualization
- Active circuit cells highlighted
- Used to verify logic correctness

---

## Planned Features
- Health system
- Enemy AI improvements & interaction logic
- Circuit completion rules
- Stamina system for pencils
- Visual polish
- Simple AI or multiplayer support (later)

---

## 🚧 Current Status

This is an **active prototype**.

# Implemented:
- Movement
- Trail drawing
- Trail erasing (partial)
- Grid logic
- Circuit defusing
- Trail type switching

# Planned:
- Circuit power propagation (BFS)
- Circuit completion detection
- Win/Loss conditions
- Trap behavior
- Map design
- Polish & balancing


## How to Run
1. Clone the repository
2. Open the project in **Unity (recommended version: 2021 LTS or later)**
3. Open the main scene
4. Press Play


## Gameplay Demo

# 1.
-- ![Gameplay demo ](Assets\demo/demo.gif)


## 📂 Notes

- Grid logic is the source of truth
- Visual trails do not guarantee connectivity
- Debug Gizmos are intentionally exposed during development
- This project is under active development.  
- Code structure and systems may change as gameplay is refined.


## Project Status
This project is currently in active development and is intended as a gameplay mechanics prototype.



## 🧠 Author

Built by **Yuvraj Nirale**  
Focused on learning **real gameplay systems**, not tutorials.





