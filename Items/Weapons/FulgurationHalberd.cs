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
	public class FulgurationHalberd : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Fulguration Halberd");
		}

		public override void SetDefaults()
		{
			Item.width = 60;
			Item.damage = 53;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 22;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 22;
			Item.useTurn = true;
			Item.knockBack = 4.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 62;
			Item.value = 125000;
			Item.rare = ItemRarityID.Pink;
		}
		
		public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.CrystalShard, 20);
	        recipe.AddIngredient(ItemID.OrichalcumHalberd);
	        recipe.AddIngredient(ItemID.HellstoneBar, 10);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	        recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.CrystalShard, 20);
	        recipe.AddIngredient(ItemID.MythrilHalberd);
	        recipe.AddIngredient(ItemID.HellstoneBar, 10);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}
