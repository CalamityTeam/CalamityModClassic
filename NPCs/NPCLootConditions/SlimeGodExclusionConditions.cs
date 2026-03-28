using CalamityModClassicPreTrailer.NPCs.SlimeGod;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.NPCs.NPCLootConditions;

public class CorePresent : IItemDropRuleCondition
{
    private NPC npc;
    public CorePresent(NPC _npc) { npc = _npc; }

    public bool CanDrop(DropAttemptInfo info) => npc.type == ModContent.NPCType<SlimeGodCore>() && !NPC.AnyNPCs(ModContent.NPCType<SlimeGodSplit>()) && !NPC.AnyNPCs(ModContent.NPCType<SlimeGodRunSplit>()) && !NPC.AnyNPCs(ModContent.NPCType<SlimeGod.SlimeGod>()) && !NPC.AnyNPCs(ModContent.NPCType<SlimeGodRun>());
    public bool CanShowItemDropInUI() => npc.type == ModContent.NPCType<SlimeGodCore>() && !NPC.AnyNPCs(ModContent.NPCType<SlimeGodSplit>()) && !NPC.AnyNPCs(ModContent.NPCType<SlimeGodRunSplit>()) && !NPC.AnyNPCs(ModContent.NPCType<SlimeGod.SlimeGod>()) && !NPC.AnyNPCs(ModContent.NPCType<SlimeGodRun>());
    public string GetConditionDescription() => null;
}

public class EbonianPresent : IItemDropRuleCondition
{
    private NPC npc;
    public EbonianPresent(NPC _npc) { npc = _npc; }

    public bool CanDrop(DropAttemptInfo info) => npc.type == ModContent.NPCType<SlimeGodSplit>() && !NPC.AnyNPCs(ModContent.NPCType<SlimeGodCore>()) && !NPC.AnyNPCs(ModContent.NPCType<SlimeGodRunSplit>()) && NPC.CountNPCS(ModContent.NPCType<SlimeGodSplit>()) < 2 && !NPC.AnyNPCs(ModContent.NPCType<SlimeGodRun>());
    public bool CanShowItemDropInUI() => npc.type == ModContent.NPCType<SlimeGodSplit>() && !NPC.AnyNPCs(ModContent.NPCType<SlimeGodCore>()) && !NPC.AnyNPCs(ModContent.NPCType<SlimeGodRunSplit>()) && NPC.CountNPCS(ModContent.NPCType<SlimeGodSplit>()) < 2 && !NPC.AnyNPCs(ModContent.NPCType<SlimeGodRun>());
    public string GetConditionDescription() => null;
}

public class CrimulanPresent : IItemDropRuleCondition
{
    private NPC npc;
    public CrimulanPresent(NPC _npc) { npc = _npc; }

    public bool CanDrop(DropAttemptInfo info) => npc.type == ModContent.NPCType<SlimeGodRunSplit>() && !NPC.AnyNPCs(ModContent.NPCType<SlimeGodCore>()) && !NPC.AnyNPCs(ModContent.NPCType<SlimeGodSplit>()) && NPC.CountNPCS(ModContent.NPCType<SlimeGodRunSplit>()) < 2 && !NPC.AnyNPCs(ModContent.NPCType<SlimeGod.SlimeGod>());
    public bool CanShowItemDropInUI() => npc.type == ModContent.NPCType<SlimeGodRunSplit>() && !NPC.AnyNPCs(ModContent.NPCType<SlimeGodCore>()) && !NPC.AnyNPCs(ModContent.NPCType<SlimeGodSplit>()) && NPC.CountNPCS(ModContent.NPCType<SlimeGodRunSplit>()) < 2 && !NPC.AnyNPCs(ModContent.NPCType<SlimeGod.SlimeGod>());
    public string GetConditionDescription() => null;
}