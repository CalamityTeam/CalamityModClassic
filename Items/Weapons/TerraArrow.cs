using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons
{
	public class TerraArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Terra Arrow");
			//Tooltip.SetDefault("Travels incredibly quickly and explodes into more arrows when it hits a certain velocity");
		}
		
		public override void SetDefaults()
		{
			Item.damage = 13;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 22;
			Item.height = 36;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 1.5f;
			Item.value = 2000;
			Item.rare = ItemRarityID.Orange;
			Item.shoot = Mod.Find<ModProjectile>("TerraArrow").Type;
			Item.shootSpeed = 15f;
			Item.ammo = 40;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(250);
			recipe.AddIngredient(null, "LivingShard");
			recipe.AddIngredient(ItemID.WoodenArrow, 250);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}