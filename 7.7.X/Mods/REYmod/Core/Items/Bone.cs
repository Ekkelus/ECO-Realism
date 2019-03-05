namespace Eco.Mods.TechTree
{
    using Shared.Localization;
    using Gameplay.Items;
    using Shared.Serialization;



    [Serialized]
    [Weight(200)]      
    [Currency]              
    public partial class BoneItem :
    Item                                     
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Bone"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Provides structure and support for the body."); } }

    }

}