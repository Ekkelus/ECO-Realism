namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Items;
    using Gameplay.Players;
    using Gameplay.Skills;
    using Gameplay.Systems.TextLinks;
    using Shared.Serialization;
    using Shared.Localization;

    [Serialized]
    public partial class StarFruitItem :
        FoodItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Star Fruit"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A magical fruit wich gives you a Specialty Star when eaten."); } }

        private static Nutrients nutrition = new Nutrients() { Carbs = 100, Fat = 100, Protein = 100, Vitamins = 100 };
        public override float Calories { get { return 0; } }
        public override Nutrients Nutrition { get { return nutrition; } }


        public override void OnUsed(Player player, ItemStack itemStack)
        {
            base.OnUsed(player, itemStack);
            player.User.SpecialtyPoints += 1;
            player.User.Tick();
        }
    }

}
    
