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
    [AutoloadEquip(EquipType.Legs)]
    public class StatigelGreaves : ModItem
{

    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Statigel Greaves");
        Item.width = 18;
        Item.height = 18;
        //AddTooltip("10% increased minion damage, increased max minions, and 10% increased movement speed");
        Item.value = 122500;
        Item.rare = 5;
        Item.defense = 8;
    }

    public override void UpdateEquip(Player player)
    {
    	player.maxMinions++;
    	player.GetDamage(DamageClass.Summon) += 10;
    	player.moveSpeed += 0.10f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "PurifiedGel", 6);
        recipe.AddIngredient(ItemID.HellstoneBar, 11);
		recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}