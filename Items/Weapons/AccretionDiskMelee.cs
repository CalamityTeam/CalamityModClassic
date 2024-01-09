using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class AccretionDiskMelee : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Elemental Disk");
			//Tooltip.SetDefault("Shred the fabric of reality!");
		}
		
		public override void SetDefaults()
		{
			Item.width = 38;
			Item.damage = 215;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 15;
			Item.knockBack = 9f;
			Item.UseSound = SoundID.Item1;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.height = 38;
			Item.value = 10000000;
			Item.rare = ItemRarityID.Red;
			Item.shoot = Mod.Find<ModProjectile>("AccretionDiskMelee").Type;
			Item.shootSpeed = 13f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "MangroveChakramMelee");
			recipe.AddIngredient(null, "FlameScytheMelee");
			recipe.AddIngredient(null, "SeashellBoomerangMelee");
			recipe.AddIngredient(null, "GalacticaSingularity", 5);
			recipe.AddIngredient(null, "BarofLife", 5);
			recipe.AddIngredient(ItemID.LunarBar, 5);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
		}
	}
}
