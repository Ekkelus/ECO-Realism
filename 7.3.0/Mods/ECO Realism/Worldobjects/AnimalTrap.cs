﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eco.Shared.Localization;
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
using Eco.Mods.TechTree;

namespace EcoRealism.Mods.ECO_Realism.Worldobjects
{
    [Serialized]
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(AnimalTrapComponent))]
    public class SmallAnimalTrapObject : WorldObject
    {
        public override string FriendlyName { get { return "Small Animal Trap"; } }


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Economy");
            this.GetComponent<PropertyAuthComponent>().Initialize(AuthModeType.Inherited);
            this.GetComponent<PublicStorageComponent>().Initialize(1);
            this.GetComponent<PublicStorageComponent>().Storage.AddRestriction(new StackLimitRestriction(1));
        }

        public override void Destroy()
        {
            base.Destroy();
        }


        protected override void PostInitialize()
        {
            base.PostInitialize();
            this.GetComponent<AnimalTrapComponent>().Initialize(new List<string>() { "Hare", "Turkey", "Fox" });
        }
    }

    [Serialized]
    [Weight(1000)]
    public partial class SmallAnimalTrapItem : WorldObjectItem<SmallAnimalTrapObject>
    {
        public override string FriendlyName { get { return "Small Animal Trap"; } }
        public override string Description { get { return "A trap to catch small animals as they run around. "; } }

        static SmallAnimalTrapItem()
        {

        }

    }


    [RequiresSkill(typeof(FishingSkill), 3)]
    public partial class SmallAnimalTrapRecipe : Recipe
    {
        public SmallAnimalTrapRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SmallAnimalTrapItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(FishingSkill), 20, FishingSkill.MultiplicativeStrategy),
                new CraftingElement<RopeItem>(typeof(FishingSkill), 4, FishingSkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(10, FishingSkill.MultiplicativeStrategy, typeof(FishingSkill), Localizer.Do("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(SmallAnimalTrapRecipe), Item.Get<SmallAnimalTrapItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<SmallAnimalTrapItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Small Animal Trap", typeof(SmallAnimalTrapRecipe));
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
}
