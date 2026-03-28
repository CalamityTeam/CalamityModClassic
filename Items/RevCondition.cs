using System;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.Items
{
    public class RevCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => CalamityWorldPreTrailer.revenge;
        
        public bool CanShowItemDropInUI() => CalamityWorldPreTrailer.revenge;
        
        public string GetConditionDescription() => "This is a Revengeance Mode drop";
    }
}