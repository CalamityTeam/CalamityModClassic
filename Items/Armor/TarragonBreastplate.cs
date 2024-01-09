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
public class TarragonBreastplate : ModItem
{

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.lifeRegen = 3;
        Item.value = 1500000;
        Item.defense = 30;
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

    public override void UpdateEquip(Player player)
    {
    	player.statLifeMax2 += 150;
        player.statManaMax2 += 100;
        player.GetDamage(DamageClass.Melee) += 0.1f;
        player.GetCritChance(DamageClass.Melee) += 10;
        player.GetDamage(DamageClass.Magic) += 0.1f;
        player.GetCritChance(DamageClass.Magic) += 10;
        player.GetDamage(DamageClass.Ranged) += 0.1f;
        player.GetCritChance(DamageClass.Ranged) += 10;
        player.GetDamage(DamageClass.Throwing) += 0.1f;
        player.GetCritChance(DamageClass.Throwing) += 10;
        player.GetDamage(DamageClass.Summon) += 0.1f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "UeliaceBar", 15);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}}