using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.NPCs.Leviathan
{
    public class NoSiren : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => !NPC.AnyNPCs(ModContent.NPCType<Siren>());
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
}