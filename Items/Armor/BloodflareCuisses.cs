using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items.Armor;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Legs)]
public class BloodflareCuisses : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Bloodflare Cuisses");
        //Tooltip.SetDefault("30% increased movement speed");
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 1762500;
        Item.defense = 29;
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

    public override void UpdateEquip(Player player)
    {
    	player.moveSpeed += 0.3f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "BloodstoneCore", 13);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}}