namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [RequiresSkill(typeof(CarpenterSkill), 0)]    
    public partial class LoggingSkill : Skill
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Logging"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

 /*        private static List<Tuple<Type, int>> ItemsGiven = new List<Tuple<Type, int>>
        {
            new Tuple<Type, int>(typeof(IronAxeItem), 1),
        };

        public override IEnumerable<Type> ItemTypesGiven { get { return ItemsGiven.Select(tuple => tuple.Item1); } }

        [Serialized] private bool HasGivenItems { get; set; }

        public override IAtomicAction CreateLevelUpAction(Player player)
        {
            if (this.Level != 0 || this.HasGivenItems)
                return base.CreateLevelUpAction(player);
            
            InventoryChangeSet changeSet = InventoryChangeSet.New(player.User.Inventory, player.User);
            foreach (Tuple<Type, int> tuple in ItemsGiven)
                changeSet.AddItems(tuple.Item1, tuple.Item2);

            SimpleAtomicAction setHasGivenItems = new SimpleAtomicAction(() => this.HasGivenItems = true);

            return new DecoratedResultAtomicAction(new MultiAtomicAction(changeSet, setHasGivenItems), (result) =>
            {
                if (result.Success)
                    return result;
                else
                    return new Result("This skill gives you " + ItemDescriptions() + "but you are unable to carry it right now.");
            });
        }

        private static string ItemDescriptions()
        {
            return ItemsGiven.Select(x => x.Item2 + " " + Item.Get(x.Item1).UILink()).InlineFoldoutList("item");
        }

        [Tooltip(151)] public string GivesItemTooltip { get { return "Grants " + ItemDescriptions(); } } */
        public static int[] SkillPointCost = { 1, 1, 1, 1, 1 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 1; } }
    }

}
