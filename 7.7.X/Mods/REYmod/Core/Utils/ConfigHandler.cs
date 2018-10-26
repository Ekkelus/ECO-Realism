//using System;
//using System.IO;
//using System.Collections.Generic;
//using System.ComponentModel;
//using Eco.Gameplay.Blocks;
//using Eco.Gameplay.Components;
//using Eco.Gameplay.Components.Auth;
//using Eco.Gameplay.DynamicValues;
//using Eco.Gameplay.Economy;
//using Eco.Gameplay.Housing;
//using Eco.Gameplay.Interactions;
//using Eco.Gameplay.Items;
//using Eco.Gameplay.Minimap;
//using Eco.Gameplay.Objects;
//using Eco.Gameplay.Players;
//using Eco.Gameplay.Property;
//using Eco.Gameplay.Skills;
//using Eco.Gameplay.Systems.TextLinks;
//using Eco.Gameplay.Pipes.LiquidComponents;
//using Eco.Gameplay.Pipes.Gases;
//using Eco.Gameplay.Systems.Tooltip;
//using Eco.Shared;
//using Eco.Shared.Math;
//using Eco.Shared.Serialization;
//using Eco.Shared.Utils;
//using Eco.Shared.View;
//using Eco.Shared.Items;
//using Eco.Gameplay.Pipes;
//using Eco.World.Blocks;
//using REYmod.Utils;
//using System.Reflection;

//namespace REYmod.Config
//{
//    /// <summary>
//    /// This class is handling the global config settings and the config file
//    /// </summary>
//    public static class ConfigHandler
//    {
//        public static string configfolderpath = "./mods/REYmod/configs/";
//        public static string configfilename = "REYmod.cfg";
//        public static string Fullconfigpath { get { return configfolderpath + configfilename; } }

//        public static void Initialize()
//        {
//            if (File.Exists(Fullconfigpath))
//            {
//                ApplyConfigFromFile();
//            }
//            UpdateConfigFile();
//        }

//        /// <summary>
//        /// Updates the Configfile by reading the current values of <see cref="REYconfig"/> and writing them to the file.
//        /// </summary>
//        public static void UpdateConfigFile()
//        {
//            List<string> lines = new List<string>();
//            foreach (FieldInfo field in typeof(REYconfig).GetFields())
//            {
//                lines.Add(field.Name + ":" + field.GetValue(null));
//            }
//            File.Delete(Fullconfigpath);
//            File.AppendAllLines(Fullconfigpath, lines);
//        }

//        /// <summary>
//        /// Returns a list of all Fieldnames of <see cref="REYconfig"/>.
//        /// </summary>
//        /// <returns></returns>
//        private static List<string> GetFieldNames()
//        {
//            List<string> fieldnames = new List<string>();
//            foreach (FieldInfo field in typeof(REYconfig).GetFields())
//            {
//                fieldnames.Add(field.Name);
//                //Console.WriteLine(field.Name);
//            }
//            return fieldnames;
//        }

//        /// <summary>
//        /// Reads the configfile and stores the values in <see cref="REYconfig"/>.
//        /// Also returns a list of the fields that weren't found in the configfile
//        /// </summary>
//        /// <returns>A list of all settings that were not in the configfile</returns>
//        private static List<string> ApplyConfigFromFile()
//        {
//            FieldInfo field;
//            List<string> missingfields = GetFieldNames();
//            string[] splitstring;
//            foreach (string line in File.ReadAllLines(Fullconfigpath))
//            {
//                try
//                {
//                    splitstring = line.Split(':');
//                    if (splitstring.Length == 2)
//                    {
//                        field = typeof(REYconfig).GetField(splitstring[0]);
//                        field.SetValue(null, Convert.ChangeType(splitstring[1], field.FieldType));
//                        missingfields.Remove(splitstring[0]);
//                    }
//                }
//                catch (Exception x)
//                {
//                    Console.WriteLine("There was an Error reading config file in line : " + line);
//                    Console.WriteLine("That line will be ignored and deleted");
//                    Console.WriteLine("Error: " + x.Message);
//                }
//            }
//            return missingfields;
//        }
//    }


//    /// <summary>
//    /// Only here to trigger the Initialization of the ConfigHandler
//    /// </summary>
//    [Category("Hidden")]
//    public class ConfigInitItem : Item
//    {
//        static ConfigInitItem()
//        {
//            ConfigHandler.Initialize();
//        }
//    }
//}