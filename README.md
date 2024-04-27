# Hunger Games Simulation

This is a **Hunger Games Simulation**! This project simulates a complex battle royale-style environment where participants compete for survival. The simulation, implemented in C#, models a wide array of factors including health, nutrition, hydration, combat abilities, and personal attributes to determine the ultimate survivor. This README provides an overview of all the variables and factors that impact the outcome of the simulation.

![image](https://github.com/itsme12453/The-Hunger-Games-Simulation/assets/74871622/93f9b76b-1ef0-4d52-92dc-5d0346b4fe9c)
![image](https://github.com/itsme12453/The-Hunger-Games-Simulation/assets/74871622/b111e07f-bcf2-4277-aae2-83ac4f210486)

## Table of Contents
- [Overview](#overview)
- [Simulation Factors](#simulation-factors)
- [Combat System](#combat-system)
- [Nutrition and Hydration](#nutrition-hydration-and-time)
- [How to Run the Simulation](#how-to-run-the-simulation)
- [Contributing](#contributing)

## Overview
The Hunger Games Simulation is a C# console program that simulates a battle royale scenario. Participants have unique characteristics and skills, and their survival depends on their health, combat skills, and ability to find food and water. The simulation evolves over time as participants fight, rest, and struggle to survive.

## Simulation Factors
The following factors contribute to the outcome of the simulation, influencing who survives and who doesn't:

- **Physical Attributes**
  - `Height`: Influences reach and agility in combat.
  - `Mass`: Affects BMI and potential strength.
  - `Reach`: Determines the range in combat scenarios.
  - `Exercise`: Impacts stamina and fitness levels.
  - `BMI`: Balance between mass and height, with implications for health and stamina.

- **Health Stats**
  - `Hydration`: Critical for survival; low hydration leads to death.
  - `Nutrition`: Necessary for maintaining energy and health.
  - `Sleep`: Affects energy recovery.
  - `Energy`: Impacts stamina and combat readiness.
  - `Health`: Overall health status; reaching zero results in death.

- **Combat Stats**
  - `Strength`: Determines damage output.
  - `Power`: Influences the effectiveness of attacks.
  - `Speed`: Affects attack and movement speed.
  - `Agility`: Determines dodge chances and flexibility in combat.
  - `Stamina`: Determines endurance in combat and running.

- **Personal Attributes**
  - `Willpower`: Impacts resistance to negative conditions and affects health decrease rates.
  - `Age`: May play a role in overall fitness and experience.

## Combat System
The combat system uses the following elements:

- **Attacks**: Randomly determined in combat rounds, with damage output influenced by strength, power, and stamina.
- **Defense**: Based on agility and stamina; higher agility means better dodging.
- **Rounds**: Combat occurs over a series of rounds, with each round allowing attacks and defense actions.
- **Fatigue**: Stamina decreases with each action, affecting attack strength and defense.


## Nutrition Hydration and Time
Participants can find food and water at random intervals:

- **Food**: Helps increase nutrition and energy levels. Finding food has a random chance, with lower nutrition increasing the probability. Lack of food can lead to death by starvation.
- **Water**: Increases hydration. Similar to food, finding water has a random chance, with lower hydration increasing the probability. Lack of water can lead to death by dehydration.
- **Time**: As time progresses, health and energy naturally decrease, requiring participants to find food and water to survive.

## How to Run the Simulation
To run the simulation, follow these steps:
1. Compile the C# code [Program.cs](https://github.com/itsme12453/The-Hunger-Games-Simulation/blob/main/simulation/simulation/Program.cs).
2. Run the compiled program.
3. Choose the simulation speed and the number of participants.
4. Observe the simulation as it progresses, with participants engaging in combat, finding food and water, and ultimately leading to a single winner.

### Simulation Speed
The simulation offers three speeds:
- **Instant**: Minimal delay between events.
- **Fast**: Moderate delay.
- **Medium**: Longer delay.

### Population Size
Choose the number of participants for the simulation. A larger population leads to a more complex simulation with more interactions and fights.

## Contributing
Contributions are welcome! If you'd like to suggest changes, please open an issue or submit a pull request. If you encounter any bugs, please report them on GitHub.
