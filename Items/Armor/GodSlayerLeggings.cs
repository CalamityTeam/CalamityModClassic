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
    public class GodSlayerLeggings : ModItem
{

    public override void SetDefaults()
    {
        //Tooltip.SetDefault("God Slayer Leggings");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("35% increased movement speed");
        Item.value = 3750000;
        Item.rare = 10;
        Item.defense = 31; //62
    }

    public override void UpdateEquip(Player player)
    {
    	player.moveSpeed += 0.35f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CosmiliteBar", 18);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}