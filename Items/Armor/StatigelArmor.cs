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
    [AutoloadEquip(EquipType.Body)]
    public class StatigelArmor : ModItem
{

    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Statigel Armor");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("5% increased throwing damage and crit chance\n50% chance to not consume thrown items");
        Item.value = 150000;
        Item.rare = 5;
        Item.defense = 10; //25
    }

    public override void UpdateEquip(Player player)
    {
        player.ThrownCost50 = true;
        player.GetDamage(DamageClass.Throwing) *= 1.05f;
        player.GetCritChance(DamageClass.Throwing) += 5;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "PurifiedGel", 8);
        recipe.AddIngredient(ItemID.HellstoneBar, 13);
		recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}