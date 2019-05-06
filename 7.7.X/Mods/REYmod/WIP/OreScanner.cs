namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Items;
    using Shared.Serialization;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Core.Utils;
    using Eco.World;
    using Eco.Shared.Math;
    using System;
    using System.Collections.Generic;
    using REYmod.Utils;
    using Eco.Gameplay.DynamicValues;
    using Eco.World.Blocks;
    using Eco.Gameplay.Plants;
    using System.Text;
    using Eco.Shared.Utils;

    [Serialized]
    [Weight(0)]
    [MaxStackSize(1)]
    public partial class OreScannerItem :
    ToolItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Ore Scanner"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Scans the ground for ores"); } }

        public override IDynamicValue SkilledRepairCost { get { return new ConstantValue(0); } }

        public override float DurabilityRate { get { return 0; } }

        [Serialized] private int scandepth = 10;

        public override bool ShouldHighlight(Type block)
        {
            if (Block.Is<Minable>(block) || Block.Is<Diggable>(block) || Block.Is<Constructed>(block)) return true;
            return false;
        }

        public override InteractResult OnActInteract(InteractionContext context)
        {
            if (scandepth >= 40) scandepth = 10;
            else scandepth += 10;
            context.Player.SendTemporaryMessage(("Scandepth changed to " + scandepth).ToLocString());

            return base.OnActInteract(context);

        }

        public override InteractResult OnActLeft(InteractionContext context)
        {
            Vector3i startposition;
            Vector3i offset = new Vector3i();
            //List<Type> blocktypes = new List<Type>();
            Dictionary<Type, int> blocktypes = new Dictionary<Type, int>();
            Block block;
            int i = 0;
            string s = "";
            string nameblock;
            if (context.HasBlock)
            {
                startposition = context.BlockPosition.Value;

                int maxdepth = (startposition.Y > scandepth) ? scandepth : startposition.Y - 1;

                for (offset.x = -2; offset.x <= 2; offset.x++)
                {
                    for (offset.z = -2; offset.z <= 2; offset.z++)
                    {
                        for (offset.y = 0; offset.y >= -maxdepth; offset.y--)
                        {
                            i++;
                            block = World.GetBlock(startposition + offset);
                            if (block != null)
                            {
                                if (block is DirtBlock || block is RockySoilBlock)
                                {
                                    if (blocktypes.ContainsKey(typeof(DirtBlock)))
                                    {
                                        blocktypes[typeof(DirtBlock)]++;
                                    }
                                    else blocktypes.Add(typeof(DirtBlock), 1);
                                }
                                else if (!(block is PlantBlock || block.Is<Constructed>() || block is WorldObjectBlock))
                                {
                                    if (blocktypes.ContainsKey(block.GetType()))
                                    {
                                        blocktypes[block.GetType()]++;
                                    }
                                    else blocktypes.Add(block.GetType(), 1);
                                }
                            }
                        }
                    }
                }

                
                List<KeyValuePair<Type, int>> resultList = (List < KeyValuePair<Type, int>>)blocktypes.AsList();
                resultList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));



                s += "Scanned " + i + " blocks<br>";
                foreach(KeyValuePair<Type,int> pair in resultList)
                {
                    Type key = pair.Key;
                    string[] tmpstrings = key.ToString().Split('.');
                    nameblock = tmpstrings[tmpstrings.Length - 1];
                    tmpstrings = nameblock.SplitOnCapitals();
                    nameblock = "";
                    foreach(string x in tmpstrings)
                    {
                        if (x != "Block") nameblock += x + " ";
                    }
                    //s += nameblock + ": " + ((float)pair.Value / i * 100).ToString("n2") + "% (" + pair.Value + ")<br>";
                    s += nameblock + ": " + ((float)pair.Value / i * 100).ToString("n2") + "%<br>";
                }
                context.Player.OpenInfoPanel("OreScanner", s);
            }


            return base.OnActRight(context);
        }


        //[Tooltip(10)]
        //public string WrittenNote(Player player)
        //{
        //    return name + "\n" + description;
        //}




    }

}