using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class PwnagehammerMelee : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Pwnagehammer");
	}

	public override void SetDefaults()
	{
		Item.width = 68;  //The width of the .png file in pixels divided by 2.
		Item.damage = 60;  //Keep this reasonable please.
		Item.noMelee = true;  //Dictates whether this is a melee-class weapon.
		Item.noUseGraphic = true;
		Item.autoReuse = true;
		Item.useAnimation = 18;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 18;
		Item.knockBack = 10f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 68;  //The height of the .png file in pixels divided by 2.
		Item.value = 300000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Pink;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("PwnagehammerMelee").Type;
		Item.shootSpeed = 12f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.Pwnhammer);
		recipe.AddIngredient(ItemID.SoulofMight, 10);
		recipe.AddIngredient(ItemID.HallowedBar, 7);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
