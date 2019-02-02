namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Items;
    using Eco.Shared.Serialization;



    [Serialized]
    [Weight(800)]
    [Currency]
    public partial class SkinnedTurkeyItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Skinned Turkey"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A skinned turkey."); } }

    }

}