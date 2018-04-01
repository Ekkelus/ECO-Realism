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

namespace EcoRealism.Utils
{
    public class SkillUtils
    {
        public static bool UserHasSkill(User user, Type skilltype, int lvl)
        {
            foreach (Skill skill in user.Skillset.Skills)
            {
                if (skill.Type == skilltype)
                {
                    if (skill.Level >= lvl)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static int SuperSkillCount(User user)
        {
            int x = 0;
            foreach (Skill skill in user.Skillset.Skills)
            {
                if (skill.Level >= 5) x++;
            }
            return x;
        }
    }

    public class ChatUtils
    { }


    public class IOUtils
    {
        public static string ReadConfig(string filename)
        {
            string content = string.Empty;
            string path = "./configs/" + filename;


            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else return "Error reading file: File does not exist!";
        }

    }
}
