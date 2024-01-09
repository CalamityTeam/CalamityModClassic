using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class XerocCuisses : ModItem
{

    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Xeroc Cuisses");
        Item.width = 18;
        Item.height = 18;
        //AddTooltip("10% increased melee, ranged, and magic critical strike chance, and 20% increased movement speed");
        //AddTooltip2("Speed of the cosmos");
        Item.value = 375000;
        Item.rare = 9;
        Item.defense = 20; //78
    }

    public override void UpdateEquip(Player player)
    {
    	player.GetCritChance(DamageClass.Melee) += 10;
    	player.GetCritChance(DamageClass.Ranged) += 10;
    	player.GetCritChance(DamageClass.Magic) += 10;
    	player.moveSpeed += 0.20f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "MeldiateBar", 11);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
    }
}}