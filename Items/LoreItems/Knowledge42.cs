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
	public class Knowledge42 : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("The Devourer of Gods");
			/* Tooltip.SetDefault("This serpent�s power to assimilate the abilities and energy of those it consumed is unique in almost all the known cosmos, save for its lesser brethren.\n" +
				"I would have soon had to eliminate it as a threat had it been given more time and creatures to feast upon.\n" +
				"Place in your inventory to boost your true melee damage by 50%."); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = 10;
			Item.consumable = false;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}
		
		public override bool CanUseItem(Player player)
		{
			return false;
		}

		public override void UpdateInventory(Player player)
		{
			CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.DoGLore = true;
		}
	}
}