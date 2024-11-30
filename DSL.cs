/******************************************************************************

Welcome to GDB Online.
GDB online is an online compiler and debugger tool for C, C++, Python, Java, PHP, Ruby, Perl,
C#, OCaml, VB, Swift, Pascal, Fortran, Haskell, Objective-C, Assembly, HTML, CSS, JS, SQLite, Prolog.
Code, Compile, Run and Debug online from anywhere in world.

*******************************************************************************/
using System;
using System.Collections.Generic;

namespace GalaxyShooterDSL
{
    // DSL Parser
    public class DSLParser
    {
        public Dictionary<string, Dictionary<string, object>> Parse(string script)
        {
            var objects = new Dictionary<string, Dictionary<string, object>>();
            var lines = script.Split('\n');
            string currentType = null;
            Dictionary<string, object> currentObject = null;

            foreach (var line in lines)
            {
                var trimmed = line.Trim();

                if (trimmed.StartsWith("#") || string.IsNullOrWhiteSpace(trimmed))
                    continue; // Skip comments and empty lines

                if (trimmed.EndsWith("{"))
                {
                    currentType = trimmed.Split(' ')[0];
                    currentObject = new Dictionary<string, object>();
                    objects[currentType] = currentObject;
                }
                else if (trimmed.EndsWith("}"))
                {
                    currentType = null;
                    currentObject = null;
                }
                else
                {
                    var parts = trimmed.Split(':');
                    var key = parts[0].Trim();
                    var value = parts[1].Trim().TrimEnd(';');

                    if (currentObject != null)
                    {
                        if (value.StartsWith("[") && value.EndsWith("]")) // Array
                        {
                            value = value.Trim('[', ']');
                            currentObject[key] = value.Split(',');
                        }
                        else
                        {
                            currentObject[key] = value;
                        }
                    }
                }
            }

            return objects;
        }
    }

    // Game Interpreter
    public class GameInterpreter
    {
        public void GenerateGameElements(Dictionary<string, Dictionary<string, object>> parsedObjects)
        {
            foreach (var obj in parsedObjects)
            {
                if (obj.Key == "PlayerShip")
                {
                    CreatePlayerShip(obj.Value);
                }
                else if (obj.Key == "EnemyWave")
                {
                    CreateEnemyWave(obj.Value);
                }
                else if (obj.Key == "PowerUp")
                {
                    CreatePowerUp(obj.Value);
                }
                else if (obj.Key == "Level")
                {
                    CreateLevel(obj.Value);
                }
            }
        }

        private void CreatePlayerShip(Dictionary<string, object> shipData)
        {
            Console.WriteLine($"Player Ship: {shipData["Name"]}, Speed: {shipData["Speed"]}, Health: {shipData["Health"]}");
            Console.WriteLine($"Weapons: {string.Join(", ", (string[])shipData["Weapons"])}");
        }

        private void CreateEnemyWave(Dictionary<string, object> waveData)
        {
            Console.WriteLine($"Enemy Wave: {waveData["Name"]}, Type: {waveData["EnemyType"]}, Count: {waveData["Count"]}, Interval: {waveData["SpawnInterval"]}s");
        }

        private void CreatePowerUp(Dictionary<string, object> powerUpData)
        {
            Console.WriteLine($"Power-Up: {powerUpData["Type"]}, Duration: {powerUpData["Duration"]}s, Location: {string.Join(", ", (string[])powerUpData["SpawnLocation"])}");
        }

        private void CreateLevel(Dictionary<string, object> levelData)
        {
            Console.WriteLine($"Level: {levelData["Name"]}, Background: {levelData["Background"]}");
            Console.WriteLine($"Waves: {string.Join(", ", (string[])levelData["Waves"])}");
            Console.WriteLine($"Power-Ups: {string.Join(", ", (string[])levelData["PowerUps"])}");
        }
    }

    // Main Program
    public class Program
    {
        public static void Main()
        {
            string script = @"
            # Define a player ship
            PlayerShip {
                Name: ""Falcon"";
                Speed: 500;
                Health: 100;
                Weapons: [""Laser"", ""Missile""];
            }

            # Define an enemy wave
            EnemyWave {
                Name: ""Wave1"";
                EnemyType: ""Fighter"";
                Count: 5;
                SpawnInterval: 2;
            }

            # Define a power-up
            PowerUp {
                Type: ""Shield"";
                Duration: 10;
                SpawnLocation: [50, 200];
            }

            # Define a level
            Level {
                Name: ""Asteroid Field"";
                Background: ""asteroid.png"";
                Waves: [""Wave1""];
                PowerUps: [""Shield""];
            }";

            DSLParser parser = new DSLParser();
            GameInterpreter interpreter = new GameInterpreter();

            Console.WriteLine("Parsing DSL script...");
            var parsedObjects = parser.Parse(script);
            Console.WriteLine("Generating game elements...\n");
            interpreter.GenerateGameElements(parsedObjects);
        }
    }
}
