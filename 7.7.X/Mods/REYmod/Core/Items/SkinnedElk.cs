namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Items;
    using Shared.Serialization;



    [Serialized]
    [Weight(1500)]
    [Currency]
    public partial class SkinnedElkItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Skinned Elk"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A skinned elk."); } }

    }

}