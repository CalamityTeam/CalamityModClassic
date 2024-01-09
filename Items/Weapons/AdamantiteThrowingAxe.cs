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
	public class AdamantiteThrowingAxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Adamantite Throwing Axe");
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.damage = 37;
			Item.DamageType = DamageClass.Throwing;
			Item.noMelee = true;
			Item.consumable = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 12;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 12;
			Item.knockBack = 3.25f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 30;
			Item.maxStack = 999;
			Item.value = 1600;
			Item.rare = ItemRarityID.LightRed;
			Item.shoot = Mod.Find<ModProjectile>("AdamantiteThrowingAxeProjectile").Type;
			Item.shootSpeed = 12f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(25);
	        recipe.AddIngredient(ItemID.AdamantiteBar);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
