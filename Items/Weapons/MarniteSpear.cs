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
	public class MarniteSpear : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Marnite Spear");
		}

		public override void SetDefaults()
		{
			Item.width = 50;
			Item.damage = 15;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.noMelee = true;
			Item.useTurn = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 21;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useTime = 21;
			Item.knockBack = 5.25f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.height = 50;
			Item.value = 65000;
			Item.rare = ItemRarityID.Green;
			Item.shoot = Mod.Find<ModProjectile>("MarniteSpearProjectile").Type;
			Item.shootSpeed = 5f;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.PlatinumBar, 5);
			recipe.AddIngredient(ItemID.Granite, 9);
			recipe.AddIngredient(ItemID.Marble, 9);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	        recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GoldBar, 5);
			recipe.AddIngredient(ItemID.Granite, 9);
			recipe.AddIngredient(ItemID.Marble, 9);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}
