using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class TarragonThrowingDart : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Tarragon Throwing Dart");
	}

	public override void SetDefaults()
	{
		Item.width = 34;  //The width of the .png file in pixels divided by 2.
		Item.damage = 170;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.consumable = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 11;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 11;
		Item.knockBack = 4.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 34;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 999;
		Item.value = 25000;  //Value is calculated in copper coins.
		Item.shoot = Mod.Find<ModProjectile>("TarragonThrowingDartProjectile").Type;
		Item.shootSpeed = 24f;
	}
	
	public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(0, 255, 200);
            }
        }
    }
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(100);
        recipe.AddIngredient(null, "UeliaceBar");
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
	}
}}
