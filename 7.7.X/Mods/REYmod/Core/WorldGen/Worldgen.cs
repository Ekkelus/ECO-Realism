using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using Eco.Gameplay.Blocks;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Minimap;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Property;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Gameplay.Pipes.LiquidComponents;
using Eco.Gameplay.Pipes.Gases;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Shared;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
	using Eco.Shared.Localization;
using Eco.Shared.Utils;
using Eco.Shared.View;
using Eco.Shared.Items;
using Eco.Gameplay.Pipes;
using Eco.World.Blocks;
using REYmod.Utils;
using Eco.Gameplay.Systems.Chat;
using System.Linq;
using Eco.Shared.Networking;
using Eco.Simulation.WorldLayers;
using Eco.World;
using Eco.WorldGenerator;
using Eco.Mods.TechTree;
using System.Timers;
using Eco.Gameplay;
using Eco.Simulation.Time;
using Eco.Shared.Localization;
using REYmod.Blocks;
using Eco.Core.Utils;

namespace REYmod.Utils
{


    public static class WorldGenUtils
    {

        private static Random random = new Random();

        /// <summary>
        /// WIP
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="block"></param>
        /// <param name="initialspread"></param>
        /// <param name="spreaddecay"></param>
        /// <param name="coreSize"></param>
        public static void CreateVein(WorldPosition3i pos, Block block, float initialspread = 1f, float spreaddecay = 0.95f, int coreSize = 0)
        {
            float newspread = initialspread * spreaddecay;   // calculate new spreadchance            
            World.SetBlock(block.GetType(), pos);           // set initial block

            foreach (Vector3i neighborpos in ((Vector3i)pos).XYZNeighbors) //iterate through all neighbouring blocks
            {
                if (World.GetBlock(neighborpos) != block) // check if block is already part of the vein
                {
                    if (initialspread > random.NextDouble())  // check spreadchance
                    {
                        CreateVein(((WorldPosition3i)neighborpos), block, newspread, spreaddecay);  // continue veingeneration with next block
                    }
                }
            }
        }


        /// <summary>
        /// Adds a block to the world, offers various parameters to describe spawnbehaviour
        /// <para/>Will never spawn on the surface.
        /// <para/>Currently only supports the placement of single blocks, veins are still WIP and not included yet
        /// </summary>
        /// <param name="block">The block that should be spawned</param>
        /// <param name="abundancyInPPM">How many blocks should be generated in average (per million)</param>
        /// <param name="minHeight">The minimum height where the block should spawn</param>
        /// <param name="maxHeight">The maximum height where the block should spawn</param>
        /// <param name="spawnOnlyIn">Newly spawned block should only replace the blocks given here</param>
        /// <param name="forcespawn">If true makes sure that a block is placed per iteration unless Block placement fails 200 consecutive times
        /// <para/> If false only tries to set the amount of blocks, so if only ~50% of positions are valid, it only spawns ~50% of blocks </param>
        public static void GenerateMineral(Type block, float abundancyInPPM, int minHeight = 0, int maxHeight = 100, Type[] spawnOnlyIn = null, bool forcespawn = true) // maybe switch from chunks to area, as chunks are 3d in Eco
        {
            string blockname = block.ToString();//(Activator.CreateInstance(block) as Block).ToString();
            blockname =  blockname.Split('.').Last();           
            int amountofveins = (int)Math.Round(World.ChunksCount * abundancyInPPM / 1000f, 0);           
            int i = 0;
            int x = 0;
            //Console.WriteLine("Should spawn " + amountofveins + " Blocks");
            Vector2i XZvector;
            Vector3i position;
            Block tmpBlock;
            using (TimedTask task = new TimedTask("Spawning " + blockname + "s (" + amountofveins + ")"))
            {
                while (i <= amountofveins)
                {
                    XZvector = World.RandomMapPos().XZ;
                    position = XZvector.X_Z((int)random.Range(minHeight, Math.Min(maxHeight, World.GetTopPos(XZvector).Y - 2)));

                    if (spawnOnlyIn != null)
                    {
                        tmpBlock = World.GetBlock(position);
                        //Console.WriteLine("Found " + tmpBlock.GetType() + " need " + spawnOnlyIn[0]);
                        if (!spawnOnlyIn.Contains(tmpBlock.GetType()))
                        {
                            if (forcespawn)
                            {
                                x++;
                                if (x >= 200)
                                {
                                    Console.WriteLine("There was a problem with while spawning custom minerals... aborting...(200 consecutive failed placement attempts)");
                                    ChatManager.ServerMessageToAllAlreadyLocalized("There was a problem while spawning custom minerals... aborting...(200 consecutive failed placement attempts)", true);
                                    break;
                                }
                                continue;
                            }
                            else i++;
                            continue;
                        }
                    }
                    //Console.WriteLine("Spawned " + blockname + " (" + i + "/" + amountofveins + ")" + " at " + position.x + "/" + position.y + "/" + position.z + "           ");
                    task.LoadPercentage = i / amountofveins;
                    World.SetBlock(block, position);
                    i++;
                    x = 0;
                }
            }
        }
    }


    public class CustomWorldGen
    {
        public static bool newworld = false;

        public static void Generate()
        {
            /* TEMPORARY DISABLED DUE TO DIAMOND BUGS!!!
            Type[] spawnin = new Type[] { typeof(CoalBlock) };
            WorldGenUtils.GenerateMineral(typeof(DiamondBlock), 300, 10, 25, spawnin);
            */ 
            newworld = false;
        }

        public static void Initialize()
        {
            //WorldGeneratorPlugin.OnFinishGenerate.Add(CustomWorldGen.Generate);
        }
    }

    public class CustomWorldGenEnabler : IWorldGenFeature
    {
        public void Generate(Random seed, Vector3 voxelSize, WorldSettings settings)
        {
            //CustomWorldGen.newworld = true;
            CustomWorldGen.Generate();
        }

    }
}