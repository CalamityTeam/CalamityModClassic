using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items.Armor;

namespace CalamityModClassic1Point0.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class AerospecBreastplate : ModItem
{
    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Aerospec Breastplate");
        Item.width = 18;
        Item.height = 18;
        //AddTooltip("+40 max mana and +1 max minions");
        Item.value = 65000;
        Item.rare = 3;
        Item.defense = 7;
    }

    public override void UpdateEquip(Player player)
    {
        player.statManaMax2 += 40;
        player.maxMinions++;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AerialiteBar", 11);
        recipe.AddIngredient(ItemID.Cloud, 10);
        recipe.AddIngredient(ItemID.RainCloud, 5);
        recipe.AddIngredient(ItemID.Feather, 2);
        recipe.AddIngredient(ItemID.Emerald, 1);
        recipe.AddTile(TileID.SkyMill);
        recipe.Register();
    }
}}