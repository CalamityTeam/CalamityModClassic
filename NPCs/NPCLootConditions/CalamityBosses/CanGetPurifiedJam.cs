using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions.CalamityBosses;

public class CanGetPurifiedJam : IItemDropRuleCondition
{
    public static NPC npc;
    public CanGetPurifiedJam(NPC _npc)
    {
        npc = _npc;
    }
    
    public bool CanDrop(DropAttemptInfo info) => !Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayerPreTrailer>().revJamDrop;
    public bool CanShowItemDropInUI() => !Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<CalamityPlayerPreTrailer>().revJamDrop;
    public string GetConditionDescription() => null;
}