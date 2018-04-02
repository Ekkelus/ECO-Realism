namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Shared.Localization;
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Core.Utils;
    using Eco.Core.Utils.AtomicAction;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;
    using Eco.Shared.Services;
    using Eco.Shared.Utils;
    using Gameplay.Systems.Tooltip;
	using EcoRealism.Utils;

    [Serialized]
    [RequiresSkill(typeof(HewingSkill), 1)]    
    public partial class HewnLogProcessingEfficiencySkill : Skill
    {
        public override string FriendlyName { get { return "Hewn Log Processing Efficiency"; } }
        public override string Description { get { return Localizer.Do(""); } }

        public static ModificationStrategy MultiplicativeStrategy = 
            new MultiplicativeStrategy(new float[] { 1, 1 - 0.1f, 1 - 0.2f, 1 - 0.3f, 1 - 0.4f, 1 - 0.5f, 1 - 0.55f, 1 - 0.6f, 1 - 0.65f, 1 - 0.7f, 1 - 0.75f });
        public static ModificationStrategy AdditiveStrategy =
            new AdditiveStrategy(new float[] { 0, 0.2f, 0.35f, 0.5f, 0.65f, 0.8f });
        public static int[] SkillPointCost = { 1, 2, 3, 4, 5, 5, 5, 5, 5, 5 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 10; } }

        public override IAtomicAction CreateLevelUpAction(Player player)
        {
            if (this.Level != 5) return base.CreateLevelUpAction(player);
            if (SkillUtils.SuperSkillCount(player.User) >= ConfigHandler.maxsuperskills) return new FailedAtomicAction(Localizer.Do("You already have enough SuperSkills " + SkillUtils.SuperSkillCount(player.User) + "/" + ConfigHandler.maxsuperskills));
            foreach (string id in SkillUtils.superskillconfirmed)
            {
                if (id == player.User.ID)
                {
                    SkillUtils.superskillconfirmed.Remove(id);
                    return base.CreateLevelUpAction(player);
                }
            }

            SkillUtils.ShowSuperSkillInfo(player);
            return new FailedAtomicAction(Localizer.Do("You need to confirm first"));

        }
    }

}
