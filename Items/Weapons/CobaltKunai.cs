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
	public class CobaltKunai : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Cobalt Kunai");
		}

		public override void SetDefaults()
		{
			Item.width = 18;
			Item.damage = 28;
			Item.DamageType = DamageClass.Throwing;
			Item.noMelee = true;
			Item.consumable = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 12;
			Item.scale = 0.75f;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 12;
			Item.knockBack = 2.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 40;
			Item.maxStack = 999;
			Item.value = 900;
			Item.rare = ItemRarityID.LightRed;
			Item.shoot = Mod.Find<ModProjectile>("CobaltKunaiProjectile").Type;
			Item.shootSpeed = 12f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(30);
	        recipe.AddIngredient(ItemID.CobaltBar);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}
