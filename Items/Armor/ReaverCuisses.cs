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
    public class ReaverCuisses : ModItem
{

    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Reaver Cuisses");
        Item.width = 18;
        Item.height = 18;
        //AddTooltip("5% increased melee critical strike chance and 20% increased movement speed");
        Item.value = 312500;
        Item.rare = 6;
        Item.defense = 12;
    }

    public override void UpdateEquip(Player player)
    {
    	player.GetCritChance(DamageClass.Melee) += 15;
    	player.moveSpeed += 0.20f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "DraedonBar", 10);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
    }
}}