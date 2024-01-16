using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class MangroveChakramMelee : ModItem
{

        public override string Texture => "CalamityModClassic1Point1/Items/Weapons/MangroveChakram";

        public override void SetDefaults()
	{
		//Tooltip.SetDefault("Mangrove Chakram");
		Item.width = 38;  //The width of the .png file in pixels divided by 2.
		Item.damage = 63;  //Keep this reasonable please.
		Item.noMelee = true;  //Dictates whether this is a melee-class weapon.
		Item.noUseGraphic = true;
		Item.useAnimation = 11;
		Item.useStyle = 1;
		Item.useTime = 11;
		////Tooltip.SetDefault("Shred 'em");
		Item.knockBack = 7.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 38;  //The height of the .png file in pixels divided by 2.
		Item.value = 500000;  //Value is calculated in copper coins.
		Item.rare = 6;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("MangroveChakramProjectileMelee").Type;
		Item.shootSpeed = 15.5f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "DraedonBar", 7);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
