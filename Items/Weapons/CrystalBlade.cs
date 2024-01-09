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
	public class CrystalBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Crystal Blade");
		}

		public override void SetDefaults()
		{
			Item.width = 40;
			Item.damage = 32;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 25;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 25;
			Item.useTurn = true;
			Item.knockBack = 4.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 40;
			Item.value = 155000;
			Item.rare = ItemRarityID.Pink;
			Item.shoot = Mod.Find<ModProjectile>("CrystalDust").Type;
			Item.shootSpeed = 3f;
		}
		
		public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.CrystalShard, 15);
	        recipe.AddIngredient(ItemID.CobaltSword);
	        recipe.AddIngredient(ItemID.PixieDust, 10);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	        recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.CrystalShard, 15);
	        recipe.AddIngredient(ItemID.PalladiumSword);
	        recipe.AddIngredient(ItemID.PixieDust, 10);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}
