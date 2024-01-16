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
    public class StatigelGreaves : ModItem
{

    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Statigel Greaves");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("10% increased minion damage and movement speed\nIncreased max minions");
        Item.value = 122500;
        Item.rare = 5;
        Item.defense = 8; //25
    }

    public override void UpdateEquip(Player player)
    {
    	player.maxMinions++;
    	player.GetDamage(DamageClass.Summon) *= 1.10f;
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