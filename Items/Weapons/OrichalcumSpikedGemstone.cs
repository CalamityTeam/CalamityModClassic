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
	public class OrichalcumSpikedGemstone : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Orichalcum Spiked Gemstone");
		}

		public override void SetDefaults()
		{
			Item.width = 14;
			Item.damage = 25;
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
			Item.height = 24;
			Item.shoot = ProjectileID.StarAnise;
			Item.maxStack = 999;
			Item.value = 1200;
			Item.rare = ItemRarityID.LightRed;
			Item.shoot = Mod.Find<ModProjectile>("OrichalcumSpikedGemstoneProjectile").Type;
			Item.shootSpeed = 12f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(50);
	        recipe.AddIngredient(ItemID.OrichalcumBar);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
