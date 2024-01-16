using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class AccretionDiskMelee : ModItem
{
		public override string Texture => "CalamityModClassic1Point1/Items/Weapons/AccretionDisk";

        public override void SetDefaults()
	{
		//Tooltip.SetDefault("Elemental Disk");
		Item.width = 38;  //The width of the .png file in pixels divided by 2.
		Item.damage = 145;  //Keep this reasonable please.
		Item.noMelee = true;  //Dictates whether this is a melee-class weapon.
		Item.noUseGraphic = true;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useStyle = 1;
		Item.useTime = 15;
		////Tooltip.SetDefault("Shred the fabric of reality!");
		Item.knockBack = 9f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 38;  //The height of the .png file in pixels divided by 2.
		Item.value = 10000000;  //Value is calculated in copper coins.
		Item.rare = 10;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("AccretionDiskMelee").Type;
		Item.shootSpeed = 25f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "MangroveChakramMelee");
		recipe.AddIngredient(null, "FlameScytheMelee");
		recipe.AddIngredient(null, "SeashellBoomerangMelee");
		recipe.AddIngredient(null, "GalacticaSingularity", 5);
		recipe.AddIngredient(null, "BarofLife", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
