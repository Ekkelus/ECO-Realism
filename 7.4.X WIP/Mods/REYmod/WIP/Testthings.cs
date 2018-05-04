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


// everything here is for still in development and is mostly used when i need some ways of testing things im working on and trying some new things
// will send a Warningmessage to all every minute if enabled!



namespace REYmod.TestStuff
{

    [Category("Hidden")]
    public class AnotherInitItem : Item
    {
        static AnotherInitItem()
        {
            GlobalEvents.OneMinuteEvent.Add(WarnDebug);
            CustomWorldGen.Initialize();
        }

        private static void WarnDebug()
        {
            ChatManager.ServerMessageToAllAlreadyLocalized("WARNING! Debugfunctions enabled! Please contact an admin about that!", false);
        }
    }


    


    public class ChatCommands : IChatCommandHandler
    {

        [ChatCommand("diamond", "Spawns a diamond above you", level: ChatAuthorizationLevel.Admin)]
        public static void DiamondSpawn(User user)
        {
            Vector3i x = user.Position.Round + new Vector3i(0, 2, 0);
            World.SetBlock(typeof(DiamondBlock), x);
        }






        [ChatCommand("testcontains", "", level: ChatAuthorizationLevel.Admin)]
        public static void Testcontains(User user,string itemstr,string ingredientstr)
        {
            Item item = Item.Get(itemstr);
            Item ingredient = Item.Get(ingredientstr);
            if(item != null && ingredient != null)
            {
                if (item.HasIngredient(ingredient.Type))
                {
                    ChatUtils.SendMessage(user, item.FriendlyName + " contains " + ingredient.FriendlyName);
                }
                else ChatUtils.SendMessage(user, item.FriendlyName + " does not contain " + ingredient.FriendlyName);

            }
            else ChatUtils.SendMessage(user, "ERROR");

        }

        [ChatCommand("forcetime", "force advances time", level: ChatAuthorizationLevel.Admin)]
        public static void ForceTime(User user,string seconds)
        {
            WorldTime.ForceAdvanceTime(Convert.ToInt32(seconds));
        }


        [ChatCommand("worldgentest", "test som worldgen functions")]
        public static void worldgentest(User user,float initialspread = 1f,float spreaddecay = 0.95f ,int coresize = 0)
        {
            Vector3 pos = user.Position + new Vector3(0,5,0);
            WorldGenUtils.CreateVein(pos.WorldPosition3i, new DirtBlock(),initialspread: initialspread, spreaddecay: spreaddecay ,coreSize: coresize);


        }

        [ChatCommand("testdebug", "test some stuff")]
        public static void testdebug(User user)
        {
            ChatUtils.SendMessage(user, World.ChunksCount.ToString());
            ChatUtils.SendMessage(user, World.GetDepth(user.Position.Round).ToString());
            ChatUtils.SendMessage(user, ("asdf").Color("green"));
            ChatUtils.SendMessage(user, Text.Color("00FF00", "bla"));
            Legislation.Laws.AllNonFailedLaws.ForEach(x => { ChatUtils.SendMessage(user, x.Title); });

            string teststring = "Hello World!";
            ChatUtils.SendMessage(user, Text.Bold(teststring));
            ChatUtils.SendMessage(user, teststring.Bold());

            Timer timer = new Timer(10000);
            timer.Disposed += (sender, e) => ChatUtils.SendMessage(user, "Timer disposed");
            timer.AutoReset = false;
            timer.Elapsed += (sender, e) => Timer_Elapsed(sender, e, user);
            timer.Start();

        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e, User user)
        {
            ChatUtils.SendMessage(user, "Timer elapsed!");
            (sender as Timer).Dispose();
        }

        [ChatCommand("testlink", "test some stuff")]
        public static void testlink(User user)
        {
            user.Player.OpenInfoPanel("agree on something", new Button(x => x.OpenInfoPanel("Test","It worked"),"TITLE","Click to agree","Agree".Color("green"),true).UILink());
        }


        [ChatCommand("spawncustom", "Finalizes worldgen by running the Custom Generator part", level: ChatAuthorizationLevel.Admin)]
        public static void SpawnCustomOres(User user, string force = null)
        {
            if (CustomWorldGen.newworld || (force == "force"))
            {
                DateTime start = DateTime.Now;
                CustomWorldGen.Generate();
                TimeSpan used = DateTime.Now - start;
                ChatUtils.SendMessage(user, "Worldgen finalized. Time spent: " + used.Minutes + ":" + used.Seconds + ":" + used.Milliseconds);
            }
            else ChatUtils.SendMessage(user, "This is not a newly generated world. This command should only be run once. You can hower force it by entering \"force\" as parameter");
        }

    }


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

            foreach(Vector3i neighborpos in ((Vector3i)pos).XYZNeighbors) //iterate through all neighbouring blocks
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
        public static void GenerateMineral(Type block, float abundancyInPPM, int minHeight = 0, int maxHeight = 100, Type[] spawnOnlyIn = null,bool forcespawn = true) // maybe switch from chunks to area, as chunks are 3d in Eco
        {
            int amountofveins = (int)Math.Round(World.ChunksCount * abundancyInPPM/1000f,0);
            int i = 0;
            int x = 0;
            Console.WriteLine("Should spawn " + amountofveins + " Blocks");
            Vector2i XZvector;
            Vector3i position;
            Block tmpBlock;
            while (i<=amountofveins)
            {
                XZvector = World.RandomMapPos().XZ;
                position = XZvector.X_Z((int)random.Range(minHeight, Math.Min(maxHeight,World.GetTopPos(XZvector).Y-2)));

                if (spawnOnlyIn != null)
                {
                    tmpBlock = World.GetBlock(position);
                    //Console.WriteLine("Found " + tmpBlock.GetType() + " need " + spawnOnlyIn[0]);
                    if (!spawnOnlyIn.Contains(tmpBlock.GetType()))
                    {
                        if (forcespawn)
                        {
                            x++;
                            if(x>=200)
                            {
                                Console.WriteLine("There was a problem with while spawning custom minerals... aborting...(200 consecutive failed placement attempts)");
                                ChatManager.ServerMessageToAllAlreadyLocalized("There was a problem with while spawning custom minerals... aborting...(200 consecutive failed placement attempts)", true);
                                break;
                            }
                            continue;
                        }
                        else i++;
                        continue;
                    }
                }
                Console.WriteLine("Spawned Block (" + i + "/"+ amountofveins + ")" + " at " + position.x + "/" + position.y + "/" + position.z);
                World.SetBlock(block, position);
                i++;
                x = 0;
            }                  
        }
    }


    public class CustomWorldGen
    {
        public static bool newworld = false;

        public static void Generate()
        {
            Type[] spawnin = new Type[] { typeof(CoalBlock) };
            DateTime start = DateTime.Now;
            WorldGenUtils.GenerateMineral(typeof(BrickFloorBlock), 500, 10, 25, spawnin);
            TimeSpan usedTime = DateTime.Now - start;
            Console.WriteLine("Worldgen finalized. Time spent: " + usedTime.Minutes + ":" + usedTime.Seconds + ":" + usedTime.Milliseconds);            
            newworld = false;
        }

        public static void Initialize()
        {
            WorldGeneratorPlugin.OnFinishGenerate.Add(CustomWorldGen.Generate);
        }
    }

    public class CustomWorldGenEnabler : IWorldGenFeature
    {
        public void Generate(Random seed, Vector3 voxelSize, WorldSettings settings)
        {
            CustomWorldGen.newworld = true;
        }

    }

}
