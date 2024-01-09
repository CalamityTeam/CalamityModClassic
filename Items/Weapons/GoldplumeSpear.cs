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
	public class GoldplumeSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Goldplume Spear");
		}

		public override void SetDefaults()
		{
			Item.width = 54;
			Item.damage = 23;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.noMelee = true;
			Item.useTurn = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 23;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useTime = 23;
			Item.knockBack = 5.75f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.height = 54;
			Item.value = 85000;
			Item.rare = ItemRarityID.Orange;
			Item.shoot = Mod.Find<ModProjectile>("GoldplumeSpearProjectile").Type;
			Item.shootSpeed = 5f;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "AerialiteBar", 10);
			recipe.AddIngredient(ItemID.SunplateBlock, 4);
	        recipe.AddTile(TileID.SkyMill);
	        recipe.Register();
		}
	}
}
