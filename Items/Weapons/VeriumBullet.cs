using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Weapons
{
	public class VeriumBullet : ModItem
	{
		
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture = "CalamityModClassic1Point0/Items/Weapons/VeriumBullet");
		    return true;
	    }
		
		public override void SetDefaults()
		{
			//DisplayName.SetDefault("Verium Bullet");
			Item.damage = 15;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			//Tooltip.SetDefault("There is no escape!");
			Item.consumable = true;
			Item.knockBack = 1.25f;
			Item.value = 500;
			Item.rare = 9;
			Item.shoot = Mod.Find<ModProjectile>("VeriumBullet").Type;
			Item.shootSpeed = 16f;
			Item.ammo = ProjectileID.Bullet;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(40);
			recipe.AddIngredient(ItemID.MusketBall, 40);
			recipe.AddIngredient(null, "VerstaltiteBar", 1);
			recipe.AddTile(null, "ParticleAccelerator");
			recipe.Register();
		}
	}
}