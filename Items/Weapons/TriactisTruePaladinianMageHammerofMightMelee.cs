using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class TriactisTruePaladinianMageHammerofMightMelee : ModItem
{

        public override string Texture => "CalamityModClassic1Point1/Items/Weapons/TriactisTruePaladinianMageHammerofMight";

        public override void SetDefaults()
	{
		//Tooltip.SetDefault("Triactis' True Paladinian Mage-Hammer of Might");
		Item.width = 160;  //The width of the .png file in pixels divided by 2.
		Item.damage = 500;  //Keep this reasonable please.
		Item.noMelee = true;  //Dictates whether this is a melee-class weapon.
		Item.noUseGraphic = true;
		Item.autoReuse = true;
		Item.useAnimation = 10;
		Item.useStyle = 1;
		Item.useTime = 10;
		////Tooltip.SetDefault("Great for impersonating devs!");
		Item.knockBack = 50f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 160;  //The height of the .png file in pixels divided by 2.
		Item.value = 100000000;  //Value is calculated in copper coins.
		Item.expert = true;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("TriactisOPHammerMelee").Type;
		Item.shootSpeed = 25f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.PaladinsHammer);
		recipe.AddIngredient(ItemID.SoulofMight, 30);
		recipe.AddIngredient(null, "ShadowspecBar", 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}
