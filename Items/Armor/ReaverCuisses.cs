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
    public class ReaverCuisses : ModItem
{

    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Reaver Cuisses");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("10% increased melee critical strike chance\n15% increased movement speed");
        Item.value = 312500;
        Item.rare = 6;
        Item.defense = 12; //51
    }

    public override void UpdateEquip(Player player)
    {
    	player.GetCritChance(DamageClass.Melee) += 10;
    	player.moveSpeed += 0.15f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "DraedonBar", 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}