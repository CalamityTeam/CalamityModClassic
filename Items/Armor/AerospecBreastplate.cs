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
public class AerospecBreastplate : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Aerospec Breastplate");
        //Tooltip.SetDefault("+20 max mana and +1 max minions");
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 65000;
        Item.rare = ItemRarityID.Orange;
        Item.defense = 7;
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