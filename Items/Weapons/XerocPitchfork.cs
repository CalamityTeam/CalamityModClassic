using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class XerocPitchfork : ModItem
{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Xeroc Pitchfork");
		}

	public override void SetDefaults()
	{
		Item.width = 48;  //The width of the .png file in pixels divided by 2.
		Item.damage = 100;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.consumable = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 19;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 19;
		Item.knockBack = 8f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 48;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 999;
		Item.value = 10000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Cyan;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("XerocPitchforkProjectile").Type;
		Item.shootSpeed = 24f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(20);
        recipe.AddIngredient(null, "MeldiateBar");
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
	}
}}
