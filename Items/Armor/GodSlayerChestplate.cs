using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items.Armor;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Body)]
public class GodSlayerChestplate : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 4250000;
        Item.defense = 36;
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

    public override void UpdateEquip(Player player)
    {
    	CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
    	modPlayer.godSlayerReflect = true;
    	player.thorns = 1f;
    	player.statLifeMax2 += 250;
        player.statManaMax2 += 150;
        player.moveSpeed += 0.15f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CosmiliteBar", 23);
        recipe.AddIngredient(null, "NightmareFuel", 11);
        recipe.AddIngredient(null, "EndothermicEnergy", 11);
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
    }
}}