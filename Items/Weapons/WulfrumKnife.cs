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
	public class WulfrumKnife : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Wulfrum Knife");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.damage = 8;
			Item.DamageType = DamageClass.Throwing;
			Item.noMelee = true;
			Item.consumable = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 15;
			Item.knockBack = 1f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 38;
			Item.maxStack = 999;
			Item.value = 100;
			Item.rare = ItemRarityID.Blue;
			Item.shoot = Mod.Find<ModProjectile>("WulfrumKnife").Type;
			Item.shootSpeed = 12f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(50);
	        recipe.AddIngredient(null, "WulfrumShard");
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}
