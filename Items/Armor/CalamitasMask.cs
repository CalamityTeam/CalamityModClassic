using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class CalamitasMask : ModItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Calamitas Mask");
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        }

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 20;
			Item.rare = 1;
			Item.vanity = true;
		}
	}
}