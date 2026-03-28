using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class VanquisherArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Vanquisher Arrow");
			/* Tooltip.SetDefault("Pierces through tiles\n" +
                "Spawns extra homing arrows as it travels"); */
		}
		
		public override void SetDefaults()
		{
			Item.damage = 33;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 14;
			Item.height = 36;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 3.5f;
			Item.value = 2250;
			Item.shoot = Mod.Find<ModProjectile>("VanquisherArrow").Type;
			Item.shootSpeed = 10f;
			Item.ammo = 40;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(250);
			recipe.AddIngredient(null, "CosmiliteBar");
			recipe.AddTile(null, "DraedonsForge");
			recipe.Register();
		}
	}
}