using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Accessories
{
	public class GrandGelatin : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Grand Gelatin");
			/* Tooltip.SetDefault("10% increased movement speed\n" +
				"100% increased jump speed\n" +
				"+20 max life and mana\n" +
				"Standing still boosts life and mana regen"); */
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = 6;
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
				player.lifeRegen += 2;
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