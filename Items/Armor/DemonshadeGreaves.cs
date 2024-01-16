using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class DemonshadeGreaves : ModItem
{
    
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Demonshade Greaves");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("Shadow speed");
        Item.value = 4900000;
        Item.expert = true;
        Item.defense = 28; //15
    }

    public override void UpdateEquip(Player player)
    {
        player.moveSpeed += 1f;
        player.accRunSpeed = 25f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "ShadowspecBar", 5);
        recipe.AddIngredient(ItemID.SoulofFright, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}