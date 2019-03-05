namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Items;
    using Shared.Serialization;



    [Serialized]
    [Weight(3000)]
    [Currency]
    public partial class SkinnedBisonItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Skinned Bison"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A skinned bison."); } }

    }

}