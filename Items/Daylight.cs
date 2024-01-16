using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items
{
	public class Daylight : ModItem
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Daylight");
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			////Tooltip.SetDefault("Summons the sun");
			Item.rare = 5;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item60;
			Item.consumable = false;
		}
		
		public override bool CanUseItem(Player player)
		{
			return !Main.dayTime;
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			Main.dayTime = true;
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.SoulofLight, 7);
			recipe.AddIngredient(ItemID.HellstoneBar, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}