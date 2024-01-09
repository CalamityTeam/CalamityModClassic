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
    public class TarragonBreastplate : ModItem
{

    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Tarragon Breastplate");
        Item.width = 18;
        Item.height = 18;
        //AddTooltip("Provides multiple offensive stat boosts");
        //AddTooltip2("Breastplate of the exiler");
        Item.lifeRegen = 3;
        Item.value = 500000;
        Item.rare = 8;
        Item.defense = 20;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Melee) += 0.1f;
        player.GetCritChance(DamageClass.Melee) += 10;
        player.GetDamage(DamageClass.Magic) += 0.1f;
        player.GetCritChance(DamageClass.Magic) += 10;
        player.GetDamage(DamageClass.Ranged) += 0.1f;
        player.GetCritChance(DamageClass.Ranged) += 10;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "UeliaceBar", 15);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
    }
}}