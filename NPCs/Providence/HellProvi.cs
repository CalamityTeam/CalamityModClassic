using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.Providence
{
    public class HellProvi : IItemDropRuleCondition
    { 
        public bool inHell()
        {
            foreach (var player in Main.ActivePlayers)
            {
                return player.ZoneUnderworldHeight;
            }
            return false;
        }
        public bool CanDrop(DropAttemptInfo info) => inHell() && Main.expertMode;
        public bool CanShowItemDropInUI() => Main.expertMode;
        public string GetConditionDescription() => "While in the Underworld";
    }
}