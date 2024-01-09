using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class TitaniumShuriken : ModItem
{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Titanium Shuriken");
		}

	public override void SetDefaults()
	{
		Item.width = 38;  //The width of the .png file in pixels divided by 2.
		Item.damage = 31;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.consumable = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 9;
		Item.scale = 0.75f;
		Item.crit = 10;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 9;
		Item.knockBack = 3f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 38;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 999;
		Item.value = 2000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.LightRed;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("TitaniumShurikenProjectile").Type;
		Item.shootSpeed = 16f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(30);
        recipe.AddIngredient(ItemID.TitaniumBar);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
