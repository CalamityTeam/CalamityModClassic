using System.Collections.Generic;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class PerforatorMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            Terraria.ID.ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        }

        public override void SetDefaults()
		{
			//Tooltip.SetDefault("Perforator Mask");
			Item.width = 28;
			Item.height = 20;
			Item.rare = 1;
			Item.vanity = true;
		}
	}
}