using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Items.PlaguebringerGoliath
{
	public class Abomination : ModItem
	{
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Abomination");
			Item.width = 28;
			Item.height = 18;
			Item.maxStack = 20;
			////Tooltip.SetDefault("Calls in the airborne jungle abomination");
			Item.rare = 8;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.useStyle = 4;
			Item.UseSound = SoundID.Item44;
			Item.consumable = true;
			Item.shoot = Mod.Find<ModProjectile>("PlaguebringerSpawn").Type;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.ZoneJungle;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "PlagueCellCluster", 10);
			recipe.AddIngredient(ItemID.IronBar, 3);
			recipe.AddIngredient(ItemID.Stinger, 2);
			recipe.AddIngredient(ItemID.Obsidian, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
			recipe = CreateRecipe();
			recipe.AddIngredient(null, "PlagueCellCluster", 10);
			recipe.AddIngredient(ItemID.LeadBar, 3);
			recipe.AddIngredient(ItemID.Stinger, 2);
			recipe.AddIngredient(ItemID.Obsidian, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}