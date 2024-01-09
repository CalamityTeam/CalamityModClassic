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
	public class PalladiumJavelin : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Palladium Javelin");
		}

		public override void SetDefaults()
		{
			Item.width = 44;
			Item.damage = 41;
			Item.DamageType = DamageClass.Throwing;
			Item.noMelee = true;
			Item.consumable = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 19;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 19;
			Item.knockBack = 5.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 44;
			Item.shoot = ProjectileID.StarAnise;
			Item.maxStack = 999;
			Item.value = 1200;
			Item.rare = ItemRarityID.LightRed;
			Item.shoot = Mod.Find<ModProjectile>("PalladiumJavelinProjectile").Type;
			Item.shootSpeed = 16f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(20);
	        recipe.AddIngredient(ItemID.PalladiumBar);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}
