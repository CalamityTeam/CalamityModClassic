using System;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.Items
{
    public class DeathCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => CalamityWorldPreTrailer.death;
        
        public bool CanShowItemDropInUI() => CalamityWorldPreTrailer.death;
        
        public string GetConditionDescription() => "This is a Death Mode drop";
    }
}