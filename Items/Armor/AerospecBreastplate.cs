using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items.Armor;

namespace CalamityModClassic1Point1.Items.Armor {
    [AutoloadEquip(EquipType.Body)]
public class AerospecBreastplate : ModItem
{
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Aerospec Breastplate");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("+20 max mana and +1 max minions");
        Item.value = 65000;
        Item.rare = 3;
        Item.defense = 6; //15
    }

    public override void UpdateEquip(Player player)
    {
        player.statManaMax2 += 20;
        player.maxMinions++;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AerialiteBar", 11);
        recipe.AddIngredient(ItemID.Cloud, 10);
        recipe.AddIngredient(ItemID.RainCloud, 5);
        recipe.AddIngredient(ItemID.Feather, 2);
        recipe.AddTile(TileID.SkyMill);
        recipe.Register();
    }
}}