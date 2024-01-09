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
public class SilvaArmor : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 8250000;
        Item.defense = 40;
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
    	player.statLifeMax2 += 300;
        player.statManaMax2 += 200;
        player.moveSpeed += 0.2f;
        player.GetCritChance(DamageClass.Melee) += 10;
		player.GetCritChance(DamageClass.Magic) += 10;
		player.GetCritChance(DamageClass.Ranged) += 10;
		player.GetCritChance(DamageClass.Throwing) += 10;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "HellcasterFragment", 20);
        recipe.AddIngredient(null, "CosmiliteBar", 10);
        recipe.AddIngredient(null, "NightmareFuel", 16);
        recipe.AddIngredient(null, "EndothermicEnergy", 16);
        recipe.AddIngredient(null, "LeadCore");
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
    }
}}