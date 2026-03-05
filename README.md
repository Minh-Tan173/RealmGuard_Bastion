# 1: Realmguard Bastion
Realmguard Bastion is a 2D Tower Defense game developed using the Unity Engine.
The project was developed with the goal of researching and implementing key gameplay systems in tower defense games, such as:
* Tower Combat System
* Ability Support System
* Progression & Unlock System
* Procedural Map Generation
  
> The entire project was **developed solo** with the goal of creating **a clean and easily scalable gameplay architecture**.

# 2: Gameplay Overview
In Realmguard Bastion, players must build defensive structures to stop waves of incoming enemies.
Each level follows a core gameplay loop:

1. Build Towers
2. Upgrade Towers
3. Defend against Enemy Waves
4. Use Abilities To support the defense
5. Complete waves to earn reawards

> Players must **strategically place towers, manage resources, and use abilities effectively** to protect their base.

# 3: Tower System
## Archer Tower
- The Archer Tower is the most basic and versatile defensive structure.
- Design goals:
  * Provide **stable** and **reliable damage**
  * Handle multiple enemy types
  * Serve as the **foundation of early-game defense**
- Key characteristics:
    *  Archer has **75° vision**
    *  Can target **all target**
    *  Balanced attack speed and damage
> Because of its flexibility, Archer Towers are commonly used to **cover multiple lanes and provide consistent damage output** throughout the game.

## Mage Tower
- The Mage Tower focuses on **high attack speed** and **scaling damage**.
- Design goals:
  * Counter fast-moving enemies
  * Scale damage significantly through upgrades
  * Handle dense enemy waves
- Key characteristics:
  * Low base damage
  * High attack speed
  * Magic projectiles that pierce through multiple enemies
> This tower excels at punishing enemies that rely on speed rather than durability.
