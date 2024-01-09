using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class BloodstoneCore : ModItem
{
	public override void SetStaticDefaults()
	{
 		//DisplayName.SetDefault("Bloodstone Core");
 	}
	
	public override void SetDefaults()
	{
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 187500;
	}
	
	public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(0, 255, 0);
            }
        }
    }
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(2);
		recipe.AddIngredient(null, "Bloodstone", 5);
		recipe.AddIngredient(null, "Phantoplasm");
		recipe.AddTile(TileID.AdamantiteForge);
		recipe.Register();
	}
}}