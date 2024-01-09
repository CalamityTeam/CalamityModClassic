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
    public class ReaverScaleMail : ModItem
{

    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Reaver Scale Mail");
        Item.width = 18;
        Item.height = 18;
        //AddTooltip("5% increased melee damage, provides life regeneration, and 8% increased melee critical strike chance");
        Item.lifeRegen = 2;
        Item.value = 300000;
        Item.rare = 6;
        Item.defense = 15;
    }

    public override void UpdateEquip(Player player)
    {
    	player.statLifeMax2 += 100;
        player.GetDamage(DamageClass.Melee) += 0.05f;
        player.GetCritChance(DamageClass.Melee) += 8;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "DraedonBar", 15);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
    }
}}