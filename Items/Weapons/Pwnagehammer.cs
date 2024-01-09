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
	public class Pwnagehammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Pwnagehammer");
		}

		public override void SetDefaults()
		{
			Item.width = 68;
			Item.damage = 60;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.autoReuse = true;
			Item.useAnimation = 18;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 18;
			Item.knockBack = 10f;
			Item.UseSound = SoundID.Item1;
			Item.DamageType = DamageClass.Throwing;
			Item.height = 68;
			Item.value = 300000;
			Item.rare = ItemRarityID.Pink;
			Item.shoot = Mod.Find<ModProjectile>("Pwnagehammer").Type;
			Item.shootSpeed = 12f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Pwnhammer);
			recipe.AddIngredient(ItemID.SoulofMight, 10);
			recipe.AddIngredient(ItemID.HallowedBar, 7);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
