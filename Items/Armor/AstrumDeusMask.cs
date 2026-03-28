using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class AstrumDeusMask : ModItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Astrum Deus Mask");
            if (Main.netMode != NetmodeID.Server)
            {
	            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
            }
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