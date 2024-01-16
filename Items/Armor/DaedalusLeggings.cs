using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items.Armor;

namespace CalamityModClassic1Point1.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class DaedalusLeggings : ModItem
{
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Daedalus Leggings");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("5% increased ranged critical strike chance\n11% increased movement speed");
        Item.value = 262500;
        Item.rare = 5;
        Item.defense = 12; //41
    }

    public override void UpdateEquip(Player player)
    {
    	player.GetCritChance(DamageClass.Ranged) += 5;
    	player.moveSpeed += 0.11f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VerstaltiteBar", 10);
		recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}