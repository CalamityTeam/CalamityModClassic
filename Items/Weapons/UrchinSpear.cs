using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class UrchinSpear : ModItem
{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Urchin Spear");
		}

	public override void SetDefaults()
	{
		Item.width = 56;  //The width of the .png file in pixels divided by 2.
		Item.damage = 17;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.useTurn = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 25;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.useTime = 25;
		Item.knockBack = 5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = false;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 56;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 45000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Green;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("UrchinSpearProjectile").Type;
		Item.shootSpeed = 4f;
	}

	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "VictideBar", 4);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}
}}
