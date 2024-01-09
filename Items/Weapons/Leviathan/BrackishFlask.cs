using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Leviathan {
public class BrackishFlask : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Brackish Flask");
		//Tooltip.SetDefault("Full of poisonous saltwater");
	}

	public override void SetDefaults()
	{
		Item.width = 28;  //The width of the .png file in pixels divided by 2.
		Item.damage = 42;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 35;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 35;
		Item.knockBack = 6.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item106;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 30;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 600000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Lime;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("BrackishFlask").Type;
		Item.shootSpeed = 12f;
	}
	
	public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "IOU");
        recipe.AddIngredient(null, "LivingShard");
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}
