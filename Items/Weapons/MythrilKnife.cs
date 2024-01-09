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
	public class MythrilKnife : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Mythril Knife");
		}

		public override void SetDefaults()
		{
			Item.width = 12;
			Item.damage = 32;
			Item.DamageType = DamageClass.Throwing;
			Item.noMelee = true;
			Item.consumable = true;
			Item.noUseGraphic = true;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 10;
			Item.knockBack = 1.75f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 30;
			Item.maxStack = 999;
			Item.value = 1100;
			Item.rare = ItemRarityID.LightRed;
			Item.shoot = Mod.Find<ModProjectile>("MythrilKnifeProjectile").Type;
			Item.shootSpeed = 12f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(40);
	        recipe.AddIngredient(ItemID.MythrilBar);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}
	}
}
