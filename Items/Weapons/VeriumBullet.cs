using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons
{
	public class VeriumBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Verium Bullet");
			//Tooltip.SetDefault("There is no escape!");
		}
		
		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 1.25f;
			Item.value = 500;
			Item.rare = ItemRarityID.Pink;
			Item.shoot = Mod.Find<ModProjectile>("VeriumBullet").Type;
			Item.shootSpeed = 16f;
			Item.ammo = 97;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(100);
			recipe.AddIngredient(ItemID.MusketBall, 100);
			recipe.AddIngredient(null, "VerstaltiteBar");
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}