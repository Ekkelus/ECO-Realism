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
using Eco.Gameplay.Systems.Chat;
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
using Eco.Shared.Localization;
using System.Linq;
using Eco.Core.Utils;
using Eco.Gameplay.Stats;
using Eco.Mods.TechTree;
using Eco.Core.Utils.AtomicAction;
using System.Timers;
using Eco.Gameplay.Wires;
using Eco.Simulation.Time;
using Eco.Core.Controller;
using REYmod.Config;

namespace REYmod.Utils
{
    public class AllergyInitItem : Item
    {
        static AllergyInitItem()
        {
            AllergyUtils.Initialize();
        }
    }


    public class AllergyUtils
    {
        public static void OnPlayerEatFoodAllergy(FoodItem food,User user)
        {
            if (REYmodSettings.Obj.Config.Foodallergiesenabled && user.IsAllergicTo(food))
            {
                ChatUtils.SendMessage(user, "You are allergic to " + food.FriendlyName + "!");
                //user.Stomach.UseCalories(food.Calories * 2);
                user.Stomach.Contents.Remove(user.Stomach.Contents.First(x => x.Food == food));
                FoodItem rottenfood = new RottenFoodItem();
                int amountofrotten = (int)(food.Calories / rottenfood.Calories);
                for (int i = 0; i < amountofrotten; i++)
                {
                    StomachEntry rottenstomachEntry = new StomachEntry { Food = rottenfood, TimeEaten = WorldTime.Seconds };
                    user.Stomach.Contents.Add(rottenstomachEntry);
                }
                //if (WorldTime.Seconds > TimeUtil.DaysToSeconds(1)) // todo: find a better solution for the update (actually.... maybe that one works too on the first day, needs testing (and removal of that if statement)
                //{
                    StomachEntry dummystomachEntry = new StomachEntry { Food = rottenfood, TimeEaten = double.MinValue }; // This is a workaround to update the nutritionvalue...
                    user.Stomach.Contents.Add(dummystomachEntry); // for some reason that Update method is private...
                    user.Stomach.CheckForBowelMovementAndExcreteFeces(user.Player); // unfortunately it wont work on the first day                   
                //}
            }
        }

        public static void Initialize()
        {
            GlobalEvents.OnPlayerEatFood.Add(OnPlayerEatFoodAllergy);
        }
    }

    public static partial class CustomClassExtensions
    {
        #region FoodItem
        [Tooltip(200)]
        public static string MytooltipSection(this FoodItem food, Player player)
        {
            //Console.WriteLine("Requested Tootip");
            if (!REYmodSettings.Obj.Config.Foodallergiesenabled) return null;
            if (player.User.IsAllergicTo(food)) return ("You are allergic to " + food.UILink() + "! (Contains " + player.User.GetAllergyItem().UILink() + ")").Color("red");
            return null;
        }

        #endregion

        #region User
        public static bool IsAllergicTo(this User user, FoodItem food)
        {
            if (user.HasState("allergy") && (user.GetState<string>("allergy") != string.Empty))
            {
                var allergenestring = user.GetState<string>("allergy");
                Item allergeneItem = Item.GetItemByString(user, allergenestring);
                Type allergenetype = allergeneItem.Type;
                if (food.HasIngredient(allergenetype)) return true;
            }
            return false;


        }

        public static Item GetAllergyItem(this User user)
        {
            if (user.HasState("allergy") && (user.GetState<string>("allergy") != string.Empty))
            {
                string allergenestring = user.GetState<string>("allergy");
                return Item.GetItemByString(user, allergenestring);
            }
            return null;
        }

        #endregion
    }
}
