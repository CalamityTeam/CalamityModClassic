using System;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.Items
{
    public class DefiledCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => CalamityWorldPreTrailer.defiled;
        
        public bool CanShowItemDropInUI() => CalamityWorldPreTrailer.defiled;
        
        public string GetConditionDescription() => "This is a Defiled Mode drop";
    }
}