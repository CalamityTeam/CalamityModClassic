using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.Providence
{
    public class HallowProvi : IItemDropRuleCondition
    {
        public bool inHallow()
        {
            foreach (var player in Main.ActivePlayers)
            {
                return player.ZoneHallow;
            }
            return false;
        }
        public bool CanDrop(DropAttemptInfo info) => inHallow() && Main.expertMode;
        public bool CanShowItemDropInUI() => Main.expertMode;
        public string GetConditionDescription() => "While in the Hallow";
    }
}