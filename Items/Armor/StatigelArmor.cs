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
    public class StatigelArmor : ModItem
{

    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Statigel Armor");
        Item.width = 18;
        Item.height = 18;
        //AddTooltip("5% increased throwing damage, 50% chance to not consume thrown items, and 5% increased throwing critical strike chance");
        Item.value = 150000;
        Item.rare = 5;
        Item.defense = 10;
    }

    public override void UpdateEquip(Player player)
    {
        player.ThrownCost50 = true;
        player.GetDamage(DamageClass.Throwing) += 0.05f;
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