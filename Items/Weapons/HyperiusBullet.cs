using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons
{
	public class HyperiusBullet : ModItem
	{
		
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture =("CalamityModClassic1Point1/Items/Weapons/HyperiusBullet");
		    return true;
	    }
		
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Hyperius Bullet");
			Item.damage = 25;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			////Tooltip.SetDefault("Your enemies might have a bad time.");
			Item.consumable = true;
			Item.knockBack = 1.5f;
			Item.value = 1000;
			Item.rare = 9;
			Item.shoot = Mod.Find<ModProjectile>("HyperiusBullet").Type;
			Item.shootSpeed = 16f;
			Item.ammo = 97;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(100);
			recipe.AddIngredient(ItemID.MusketBall, 100);
			recipe.AddIngredient(null, "BarofLife");
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}