using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace CalamityModClassicPreTrailer.Items;

public class SkeletronCondition :  IItemDropRuleCondition
{
    public bool CanDrop(DropAttemptInfo info) => NPC.downedBoss3;
    public bool CanShowItemDropInUI() => NPC.downedBoss3;
    public string GetConditionDescription() =>"After defeating Skeletron";
    
}