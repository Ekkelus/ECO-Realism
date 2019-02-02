namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Items;
    using Eco.Shared.Serialization;



    [Serialized]
    [Weight(300)]
    [Currency]
    public partial class SkinnedHareItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Skinned Hare"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A skinned hare."); } }

    }

}