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
	public class Cosmolight : ModItem
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Cosmolight");
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			////Tooltip.SetDefault("Changes night to day and vice versa");
			Item.rare = 5;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item60;
			Item.consumable = false;
		}
		
		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
		{
			if (Main.dayTime)
			{
				Main.dayTime = false;
			}
			else if (!Main.dayTime)
			{
				Main.dayTime = true;
			}
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Daylight");
			recipe.AddIngredient(null, "Moonlight");
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}