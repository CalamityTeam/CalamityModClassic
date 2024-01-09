using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons
{
	public class FrostsparkBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Frostspark Bullet");
			//Tooltip.SetDefault("Has a chance to freeze enemies and explode into electicity\nEnemies that are immune to being frozen take more damage from these bullets");
		}

		public override void SetDefaults()
		{
			Item.damage = 11;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 1.25f;
			Item.value = 600;
			Item.rare = ItemRarityID.Pink;
			Item.shoot = Mod.Find<ModProjectile>("FrostsparkBullet").Type;
			Item.shootSpeed = 14f;
			Item.ammo = 97;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(150);
			recipe.AddIngredient(ItemID.MusketBall, 150);
			recipe.AddIngredient(null, "CryoBar");
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}