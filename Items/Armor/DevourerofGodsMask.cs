using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.ID;

namespace CalamityModClassic1Point2.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class DevourerofGodsMask : ModItem
	{
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Devourer of Gods Mask");
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        }

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 20;
			Item.rare = ItemRarityID.Blue;
			Item.vanity = true;
		}
	}
}