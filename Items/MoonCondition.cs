using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.Items
{
    public class MoonCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => NPC.downedMoonlord;
        public bool CanShowItemDropInUI() => NPC.downedMoonlord;
        public string GetConditionDescription() =>"After defeating Moon Lord";
    
    }
}