using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class TerraBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terra Bullet");
			// Tooltip.SetDefault("Explodes and splits into homing terra shards on death");
		}
		
		public override void SetDefaults()
		{
			Item.damage = 9;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 1.25f;
			Item.value = 2000;
			Item.rare = 7;
			Item.shoot = Mod.Find<ModProjectile>("TerraBullet").Type;
			Item.shootSpeed = 10f;
			Item.ammo = 97;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(100);
			recipe.AddIngredient(ItemID.CrystalBullet, 100);
			recipe.AddIngredient(null, "LivingShard");
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}