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
public class BloodflareBodyArmor : ModItem
{

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 1900000;
        Item.defense = 33;
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
    	player.statLifeMax2 += 100;
        player.statManaMax2 += 100;
        player.GetDamage(DamageClass.Melee) += 0.14f;
        player.GetCritChance(DamageClass.Melee) += 14;
        player.GetDamage(DamageClass.Magic) += 0.14f;
        player.GetCritChance(DamageClass.Magic) += 14;
        player.GetDamage(DamageClass.Ranged) += 0.14f;
        player.GetCritChance(DamageClass.Ranged) += 14;
        player.GetDamage(DamageClass.Throwing) += 0.14f;
        player.GetCritChance(DamageClass.Throwing) += 14;
        player.GetDamage(DamageClass.Summon) += 0.14f;
        if (player.lavaWet == true)
    	{
        	player.statDefense += 30;
        	player.lifeRegen += 10;
        }
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "BloodstoneCore", 16);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}}