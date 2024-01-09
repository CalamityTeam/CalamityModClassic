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
public class SilvaLeggings : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Silva Leggings");
        //Tooltip.SetDefault("45% increased movement speed");
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 7750000;
        Item.defense = 36;
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

    public override void UpdateEquip(Player player)
    {
    	player.moveSpeed += 0.45f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "HellcasterFragment", 15);
        recipe.AddIngredient(null, "CosmiliteBar", 7);
        recipe.AddIngredient(null, "NightmareFuel", 15);
        recipe.AddIngredient(null, "EndothermicEnergy", 15);
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
    }
}}