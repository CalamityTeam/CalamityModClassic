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
	public class HellionFlowerSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Hellion Flower Spear");
		}

		public override void SetDefaults()
		{
			Item.width = 74;
			Item.damage = 67;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.noMelee = true;
			Item.useTurn = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useTime = 20;
			Item.knockBack = 7.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.height = 74;
			Item.value = 355000;
			Item.rare = ItemRarityID.LightPurple;
			Item.shoot = Mod.Find<ModProjectile>("HellionFlowerSpearProjectile").Type;
			Item.shootSpeed = 8f;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "DraedonBar", 12);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
