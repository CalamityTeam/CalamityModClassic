using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Weapons
{
	public class HyperiusBullet : ModItem
	{
		
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture = "CalamityModClassic1Point0/Items/Weapons/HyperiusBullet");
		    return true;
	    }
		
		public override void SetDefaults()
		{
			//DisplayName.SetDefault("Hyperius Bullet");
			Item.damage = 25;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 12;
			Item.height = 12;
			Item.maxStack = 999;
			//Tooltip.SetDefault("Your enemies might have a bad time.");
			Item.consumable = true;
			Item.knockBack = 1.5f;
			Item.value = 500;
			Item.rare = 9;
			Item.shoot = Mod.Find<ModProjectile>("HyperiusBullet").Type;
			Item.shootSpeed = 16f;
			Item.ammo = Mod.Find<ModProjectile>("HyperiusBullet").Type;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(300);
			recipe.AddIngredient(ItemID.MusketBall, 300);
			recipe.AddIngredient(null, "BarofLife", 1);
			recipe.AddTile(null, "ParticleAccelerator");
			recipe.Register();
		}
	}
}