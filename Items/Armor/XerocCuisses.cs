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
    public class XerocCuisses : ModItem
{

    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Xeroc Cuisses");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("10% increased critical strike chance\n30% increased movement speed\nSpeed of the cosmos");
        Item.value = 375000;
        Item.rare = 9;
        Item.defense = 17; //62
    }

    public override void UpdateEquip(Player player)
    {
    	player.GetCritChance(DamageClass.Melee) += 10;
    	player.GetCritChance(DamageClass.Ranged) += 10;
    	player.GetCritChance(DamageClass.Throwing) += 10;
    	player.GetCritChance(DamageClass.Magic) += 10;
    	player.moveSpeed += 0.30f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "MeldiateBar", 11);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}