using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.NPCs.Leviathan
{
    public class NoLevi : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info) => !NPC.AnyNPCs(ModContent.NPCType<Leviathan>());
        public bool CanShowItemDropInUI() => true;
        public string GetConditionDescription() => null;
    }
}