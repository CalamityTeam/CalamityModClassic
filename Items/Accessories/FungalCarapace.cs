using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories
{
	public class FungalCarapace : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Fungal Carapace");
			//Tooltip.SetDefault("You emit a mushroom spore explosion when you are hit");
		}
		
		public override void SetDefaults()
		{
			Item.defense = 2;
			Item.width = 20;
			Item.height = 24;
			Item.value = 30000;
			Item.rare = ItemRarityID.Blue;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
			modPlayer.fCarapace = true;
		}
	}
}