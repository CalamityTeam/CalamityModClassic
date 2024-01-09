using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class TruePaladinsHammer : ModItem
{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("True Paladin's Hammer");
		}

	public override void SetDefaults()
	{
		Item.width = 14;  //The width of the .png file in pixels divided by 2.
		Item.damage = 120;  //Keep this reasonable please.
		Item.noMelee = true;  //Dictates whether this is a melee-class weapon.
		Item.noUseGraphic = true;
		Item.autoReuse = true;
		Item.useAnimation = 13;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 13;
		Item.knockBack = 20f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.DamageType = DamageClass.Throwing;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 28;  //The height of the .png file in pixels divided by 2.
		Item.value = 9000000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Cyan;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("OPHammer").Type;
		Item.shootSpeed = 14f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.PaladinsHammer);
		recipe.AddIngredient(null, "CalamityDust", 5);
		recipe.AddIngredient(null, "CoreofChaos", 5);
		recipe.AddIngredient(null, "CruptixBar", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
