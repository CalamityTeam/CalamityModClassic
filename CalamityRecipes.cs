using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Tiles;

namespace CalamityModClassic1Point0
{
    public class CalamityRecipes : ModSystem
    {
    	
    	public override void AddRecipes()/* tModPorter Note: Removed. Use ModSystem.AddRecipes */
		{
			Recipe recipe = Recipe.Create(ItemID.SkyMill);
			recipe.AddIngredient(ItemID.SunplateBlock, 10);
			recipe.AddIngredient(ItemID.Cloud, 5);
			recipe.AddIngredient(ItemID.RainCloud, 3);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	        recipe = Recipe.Create(ItemID.IceMachine);
			recipe.AddIngredient(ItemID.IceBlock, 25);
			recipe.AddIngredient(ItemID.SnowBlock, 15);
			recipe.AddIngredient(ItemID.IronBar, 3);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	        recipe = Recipe.Create(ItemID.Starfury);
			recipe.AddIngredient(ItemID.GoldBroadsword);
			recipe.AddIngredient(ItemID.FallenStar, 10);
			recipe.AddIngredient(null, "VictoryShard", 3);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	        recipe = Recipe.Create(ItemID.Starfury);
			recipe.AddIngredient(ItemID.PlatinumBroadsword);
			recipe.AddIngredient(ItemID.FallenStar, 10);
			recipe.AddIngredient(null, "VictoryShard", 3);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	        recipe = Recipe.Create(ItemID.CloudinaBottle);
			recipe.AddIngredient(null, "DiamondDust", 50);
			recipe.AddIngredient(ItemID.Bottle);
			recipe.AddIngredient(ItemID.Cloud, 25);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	        recipe = Recipe.Create(ItemID.BlizzardinaBottle);
			recipe.AddIngredient(null, "DiamondDust", 100);
			recipe.AddIngredient(ItemID.Bottle);
			recipe.AddIngredient(ItemID.SnowBlock, 50);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	        recipe = Recipe.Create(ItemID.SandstorminaBottle);
			recipe.AddIngredient(null, "DesertFeather", 10);
			recipe.AddIngredient(null, "DiamondDust", 200);
			recipe.AddIngredient(ItemID.Bottle);
			recipe.AddIngredient(ItemID.SandBlock, 70);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	        recipe = Recipe.Create(ItemID.ShinyRedBalloon);
			recipe.AddIngredient(ItemID.WhiteString);
			recipe.AddIngredient(ItemID.Gel, 50);
			recipe.AddIngredient(ItemID.Cloud, 20);
	        recipe.AddTile(TileID.Solidifier);
	        recipe.Register();
		}
    }
}