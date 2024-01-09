using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items {
public class ShadowspecBar : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Shadowspec Bar");
		Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(8, 9));
	}
		
	public override void SetDefaults()
	{
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 1000000;
	}
	
	public override void ModifyTooltips(List<TooltipLine> list)
    {	        
		foreach (TooltipLine line2 in list)
	    {
	        if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	        {
	            line2.OverrideColor = new Color(108, 45, 199);
	        }
	    }
	}
	
	public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe(3);
        recipe.AddIngredient(null, "BarofLife", 3);
        recipe.AddIngredient(null, "Phantoplasm", 3);
        recipe.AddIngredient(null, "NightmareFuel", 3);
        recipe.AddIngredient(null, "EndothermicEnergy", 3);
        recipe.AddIngredient(null, "CalamitousEssence");
        recipe.AddIngredient(null, "DarksunFragment");
        recipe.AddIngredient(null, "HellcasterFragment");
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
	}
}}