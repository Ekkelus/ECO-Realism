using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using Eco.Gameplay.Systems.TextLinks;
using Eco.Mods.TechTree;
using Eco.Shared.Networking;
using Eco.Shared.Utils;

namespace REYmod.Utils
{

    public class RubbleRemover : IChatCommandHandler
    {


        [ChatCommand("removerubble", "MOD/ADMIN only! - Opens a Menu for removing Rubble", ChatAuthorizationLevel.User)]
        public static void RemoveRubble(User user)
        {
            if (!user.IsAdmin && !user.GetState<bool>("Moderator"))//admin/mod only
            {
                ChatUtils.SendMessage(user, "Not Authorized to use this command!");
                return;
            }


            IEnumerable<RubbleObject<StoneItem>> stonerubble = NetObjectManager.GetObjectsOfType<RubbleObject<StoneItem>>();
            IEnumerable<RubbleObject<IronOreItem>> ironrubble = NetObjectManager.GetObjectsOfType<RubbleObject<IronOreItem>>();
            IEnumerable<RubbleObject<CopperOreItem>> copperrubble = NetObjectManager.GetObjectsOfType<RubbleObject<CopperOreItem>>();
            IEnumerable<RubbleObject<GoldOreItem>> goldrubble = NetObjectManager.GetObjectsOfType<RubbleObject<GoldOreItem>>();
            IEnumerable<RubbleObject<CoalItem>> coalrubble = NetObjectManager.GetObjectsOfType<RubbleObject<CoalItem>>();

            /* Maybe this is a better solution, it would be nice to dynamically find all types of RubbleObject<>, right now i have to declare every single one separately here, i left out diamonds for example
            IEnumerable<RubbleObject> rubbleObjects = NetObjectManager.GetObjectsOfType<RubbleObject>();
            rubbleObjects.Where(x => { return (x is RubbleObject<StoneItem>); });
            */


            //stonerubble.ForEach<RubbleObject>(x => NetObjectManager.Remove(x));
            string buttontext = "";
            IEnumerable<RubbleObject> rubbles;
            
            string panelcontent = "Rubble Overview: <br>";

            
            rubbles = stonerubble;

            panelcontent += "<br>StoneRubble\t" + rubbles.Count().ToString().PadLeft(6) + "\t\t";
            buttontext = (rubbles.Count() != 0) ? "<color=red>Remove</color>" : "<color=green>CLEAR</color>";
            panelcontent += new Button(player => { DestroyRubbleType<RubbleObject<StoneItem>>(user, "Stone"); RemoveRubble(user); }, content: buttontext , singleuse: true).UILink();

            rubbles = ironrubble;
            panelcontent += "<br>IronRubble   \t" + rubbles.Count().ToString().PadLeft(6) + "\t\t";
            buttontext = (rubbles.Count() != 0) ? "<color=red>Remove</color>" : "<color=green>CLEAR</color>";
            panelcontent += new Button(player => { DestroyRubbleType<RubbleObject<IronOreItem>>(user, "Iron"); RemoveRubble(user); }, content: buttontext, singleuse: true).UILink();

            rubbles = copperrubble;
            panelcontent += "<br>CopperRubble\t" + rubbles.Count().ToString().PadLeft(6) + "\t\t";
            buttontext = (rubbles.Count() != 0) ? "<color=red>Remove</color>" : "<color=green>CLEAR</color>";
            panelcontent += new Button(player => { DestroyRubbleType<RubbleObject<CopperOreItem>>(user, "Copper"); RemoveRubble(user); }, content: buttontext, singleuse: true).UILink();

            rubbles = goldrubble;
            panelcontent += "<br>GoldRubble\t" + rubbles.Count().ToString().PadLeft(6) + "\t\t";
            buttontext = (rubbles.Count() != 0) ? "<color=red>Remove</color>" : "<color=green>CLEAR</color>";
            panelcontent += new Button(player => { DestroyRubbleType<RubbleObject<GoldOreItem>>(user, "Gold"); RemoveRubble(user); }, content: buttontext, singleuse: true).UILink();

            rubbles = coalrubble;
            panelcontent += "<br>CoalRubble\t" + rubbles.Count().ToString().PadLeft(6) + "\t\t";
            buttontext = (rubbles.Count() != 0) ? "<color=red>Remove</color>" : "<color=green>CLEAR</color>";
            panelcontent += new Button(player => { DestroyRubbleType<RubbleObject<CoalItem>>(user,"Coal"); RemoveRubble(user); }, content: buttontext, singleuse: true).UILink();

            user.Player.OpenInfoPanel("Rubble Remover", panelcontent);

        }


        // this is not mine.. i dont know why my own implementation didn't work, i'll use this for now
        public static void DestroyRubbleType<T>(User user = null, string type = "") where T : RubbleObject
        {            
            MethodInfo destroyRubbleMethod = typeof(RubbleObject).GetMethod("Destroy", BindingFlags.NonPublic | BindingFlags.Instance);
            if (destroyRubbleMethod == null)
            {
                throw new Exception("Destroy method not found on Rubble type");
            }
            IEnumerable<T> objects = NetObjectManager.GetObjectsOfType<T>();
            var count = 0;
            objects.ForEach(rubbleObject =>
            {
                count += 1;
                destroyRubbleMethod.Invoke(rubbleObject, new Object[0]);
            });
            //return count;
            if (user != null) IOUtils.WriteCommandLog(user, "DestroyRubble", "Removed " + count +" " + type+"-Rubbles");
        }



    }
}
