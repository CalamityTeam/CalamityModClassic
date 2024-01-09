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
	public class GrandGelatin : ModItem
	{		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.value = 300000;
			Item.rare = ItemRarityID.LightPurple;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.moveSpeed += 0.1f;
        	player.jumpSpeedBoost += 1.0f;
        	player.statLifeMax2 += 20;
        	player.statManaMax2 += 20;
			if ((double)Math.Abs(player.velocity.X) < 0.05 && (double)Math.Abs(player.velocity.Y) < 0.05 && player.itemAnimation == 0)
			{
				player.lifeRegen += 5;
				player.manaRegenBonus += 2;
			}
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "ManaJelly");
			recipe.AddIngredient(null, "LifeJelly");
			recipe.AddIngredient(null, "VitalJelly");
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}