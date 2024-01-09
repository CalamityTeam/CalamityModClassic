using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Cryogen {
public class IceStar : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ice Star");
		//Tooltip.SetDefault("Ice Stars are too brittle to be recovered after being thrown");
	}

	public override void SetDefaults()
	{
		Item.width = 62;  //The width of the .png file in pixels divided by 2.
		Item.damage = 35;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.consumable = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 12;
		Item.crit = 7;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 12;
		Item.knockBack = 2.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 62;  //The height of the .png file in pixels divided by 2.
		Item.scale = 0.65f;
		Item.maxStack = 999;
		Item.value = 3000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Pink;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("IceStarProjectile").Type;
		Item.shootSpeed = 14f;
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(50);
        recipe.AddIngredient(null, "CryoBar");
        recipe.AddTile(TileID.IceMachine);
        recipe.Register();
	}
}}
