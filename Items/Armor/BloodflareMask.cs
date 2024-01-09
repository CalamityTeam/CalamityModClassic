using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items.Armor;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Head)]
public class BloodflareMask : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 1750000;
        Item.defense = 28; //83
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

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("BloodflareBodyArmor").Type && legs.type == Mod.Find<ModItem>("BloodflareCuisses").Type;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawShadowSubtle = true;
    }

    public override void UpdateArmorSet(Player player)
    {
    	CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
    	modPlayer.bloodflareSet = true;
        player.setBonus = "Greatly increases life regen\n" +
        	"Enemies are more likely to target you\n" +
        	"Enemies below 50% life have a chance to drop hearts when struck\n" +
        	"Enemies above 50% life have a chance to drop mana stars when struck\n" +
        	"Enemies killed during a Blood Moon have a much higher chance to drop Blood Orbs\n" +
        	"Blood Moons have a 33% chance of occurring each night";
        player.crimsonRegen = true;
        player.aggro += 900;
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.maxMinions += 3;
    	player.lavaImmune = true;
    	player.ignoreWater = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "BloodstoneCore", 11);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}}