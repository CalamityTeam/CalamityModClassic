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
	public class FeatherKnife : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Feather Knife");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.damage = 17;
			Item.DamageType = DamageClass.Throwing;
			Item.noMelee = true;
			Item.consumable = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 13;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 13;
			Item.knockBack = 2f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 32;
			Item.maxStack = 999;
			Item.value = 300;
			Item.rare = ItemRarityID.Orange;
			Item.shoot = Mod.Find<ModProjectile>("FeatherKnifeProjectile").Type;
			Item.shootSpeed = 12f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(30);
	        recipe.AddIngredient(null, "AerialiteBar");
	        recipe.AddTile(TileID.SkyMill);
	        recipe.Register();
		}
	}
}
