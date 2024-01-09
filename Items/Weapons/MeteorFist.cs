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
	public class MeteorFist : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Meteor Fist");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.damage = 15;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.noMelee = true;
			Item.useTurn = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useTime = 30;
			Item.knockBack = 5.75f;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.height = 28;
			Item.value = 75000;
			Item.rare = ItemRarityID.Green;
			Item.shoot = Mod.Find<ModProjectile>("MeteorFist").Type;
			Item.shootSpeed = 10f;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.MeteoriteBar, 10);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}
