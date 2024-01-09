using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class UeliaceBar : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ueliace Bar");
	}
		
	public override void SetDefaults()
	{
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 218750;
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
		Recipe recipe = CreateRecipe(4);
		recipe.AddIngredient(null, "UelibloomOre", 12);
		recipe.AddIngredient(null, "UnholyEssence");
		recipe.AddTile(TileID.AdamantiteForge);
		recipe.Register();
	}
}}