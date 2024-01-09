using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Accessories {
public class Sponge : ModItem
{
	public override void SetDefaults()
	{
		Item.defense = 20;
		Item.width = 20;
		Item.height = 20;
		Item.value = 10000000;
		Item.accessory = true;
	}
	
	public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(43, 96, 222);
            }
        }
    }
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		modPlayer.beeResist = true;
		modPlayer.aSpark = true;
		modPlayer.gShell = true;
		modPlayer.fCarapace = true;
		modPlayer.absorber = true;
		modPlayer.aAmpoule = true;
		player.statManaMax2 += 30;
		
	}
	
	public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "TheAbsorber");
        recipe.AddIngredient(null, "AmbrosialAmpoule");
        recipe.AddIngredient(null, "CosmiliteBar", 15);
        recipe.AddIngredient(null, "Phantoplasm", 15);
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
    }
}}