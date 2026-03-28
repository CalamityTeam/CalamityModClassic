using System;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.Items
{
    public class ProvCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => CalamityWorldPreTrailer.downedProvidence;
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() =>  "After defeating Providence";
    }
}