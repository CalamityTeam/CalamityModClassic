using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons
{
	public class VeriumBullet : ModItem
	{
		
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture =("CalamityModClassic1Point1/Items/Weapons/VeriumBullet");
		    return true;
	    }
		
		public override void SetDefaults()
		{
			//Tooltip.SetDefault("Verium Bullet");
			Item.damage = 15;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 999;
			////Tooltip.SetDefault("There is no escape!");
			Item.consumable = true;
			Item.knockBack = 1.25f;
			Item.value = 500;
			Item.rare = 5;
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