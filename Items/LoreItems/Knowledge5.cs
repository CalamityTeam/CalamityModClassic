using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Items.LoreItems
{
	public class Knowledge5 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Crabulon");
			/* Tooltip.SetDefault("A crab and its mushrooms, a love story.\n" +
                "It's interesting how creatures can adapt given certain circumstances.\n" +
				"Place in your inventory to gain the Mushy buff while underground or in the mushroom biome."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 2;
			Item.consumable = false;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			if (player.ZoneGlowshroom || player.ZoneDirtLayerHeight || player.ZoneRockLayerHeight)
			{
				if (Main.myPlayer == player.whoAmI)
				{
					player.AddBuff(Mod.Find<ModBuff>("Mushy").Type, 2);
				}
			}
		}
	}
}