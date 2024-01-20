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
    public class GodSlayerChestplate : ModItem
{

    public override void SetDefaults()
    {
        //Tooltip.SetDefault("God Slayer Chestplate");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("+150 max mana\n+250 max life\n15% increased movement speed\nEnemies take immense damage when they hit you\nAttacks have a 5% chance to do no damage to you");
        Item.value = 4250000;
        Item.rare = 10;
        Item.defense = 36; //62
    }

    public override void UpdateEquip(Player player)
    {
    	CalamityPlayer1Point1 modPlayer = player.GetModPlayer<CalamityPlayer1Point1>();
    	modPlayer.godSlayerReflect = true;
    	player.thorns = 10f;
    	player.statLifeMax2 += 250;
        player.statManaMax2 += 150;
        player.moveSpeed += 0.15f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CosmiliteBar", 23);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}