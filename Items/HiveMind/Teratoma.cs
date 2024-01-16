using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.HiveMind
{
	public class Teratoma : ModItem
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Teratoma");
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 20;
			////Tooltip.SetDefault("Summons the brain eater");
			Item.rare = 3;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.shoot = Mod.Find<ModProjectile>("HiveMindSpawn").Type;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneCorrupt;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.RottenChunk, 9);
			recipe.AddIngredient(ItemID.ShadowScale, 5);
			recipe.AddIngredient(ItemID.DemoniteBar, 2);
			recipe.AddTile(TileID.DemonAltar);
			recipe.Register();
		}
	}
}