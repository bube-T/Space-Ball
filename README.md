# Space Ball

A 3D physics game built in **Unity 2022.3 (URP)**. Pilot a thruster-powered ball from the launch pad to the landing pad on each level — before the timer runs out and without crashing into obstacles, moving platforms, or enemy drones.

## How to open & run

1. Open the project folder in **Unity Hub** (made with 2022.3.57f1 — any 2022.3 LTS version works).
2. Open `Assets/Scenes/Main menu.unity`.
3. Press **Play**.

Build order (File → Build Settings): `Main menu` → `Level1` → `Level2` → `Level3`.

## Controls

| Key | Action |
|-----|--------|
| **Space** | Thrust |
| **A / D** (or arrow keys) | Rotate left / right |
| **R** | Restart current level |
| **Esc** | Return to main menu (or close the level-select panel) |
| **G** | Skip to next level *(debug builds / editor only)* |

## Gameplay

- Land on the pad tagged **Finish** to complete a level. Beating Level 3 returns you to the menu.
- Touching anything except **Friendly**-tagged objects counts as a crash and restarts the level.
- Each level has a **countdown timer** (turns red in the last 10 seconds). If it hits zero, the level restarts.
- Watch out for patrolling ground enemies, chasing drones, and turrets that shoot at you.

## Project layout

| Path | What's there |
|------|--------------|
| `Assets/Scripts/` | All gameplay code (movement, collisions, timer, enemies, menu) |
| `Assets/Scenes/` | Main menu + 3 levels |
| `Assets/Game Assets/` | Models, materials, drone prefabs, environment art |
| `Assets/Audio/` | Thrust, success, crash and menu sounds |
| `Assets/Wispy Sky/` | Skybox |
