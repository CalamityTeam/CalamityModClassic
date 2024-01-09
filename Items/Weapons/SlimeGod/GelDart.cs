using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.SlimeGod {
public class GelDart : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Gel Dart");
	}

	public override void SetDefaults()
	{
		Item.width = 14;  //The width of the .png file in pixels divided by 2.
		Item.damage = 23;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.consumable = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 11;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 11;
		Item.knockBack = 2.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 28;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 999;
		Item.value = 1000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Pink;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("GelDartProjectile").Type;
		Item.shootSpeed = 12f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(150);
        recipe.AddIngredient(null, "PurifiedGel", 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}
}}
