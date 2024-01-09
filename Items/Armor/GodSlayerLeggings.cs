using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Legs)]
public class GodSlayerLeggings : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("God Slayer Leggings");
        //Tooltip.SetDefault("35% increased movement speed");
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 3750000;
        Item.defense = 31;
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
    	player.moveSpeed += 0.35f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CosmiliteBar", 18);
        recipe.AddIngredient(null, "NightmareFuel", 9);
        recipe.AddIngredient(null, "EndothermicEnergy", 9);
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
    }
}}