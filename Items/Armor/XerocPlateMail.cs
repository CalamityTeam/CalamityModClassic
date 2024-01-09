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
    public class XerocPlateMail : ModItem
{

    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Xeroc Plate Mail");
        Item.width = 18;
        Item.height = 18;
        //AddTooltip("+80 max mana and 12% increased movement speed");
        //AddTooltip2("Armor of the cosmos");
        Item.value = 425000;
        Item.rare = 9;
        Item.defense = 27;
    }

    public override void UpdateEquip(Player player)
    {
        player.statManaMax2 += 80;
        player.moveSpeed += 0.12f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "MeldiateBar", 15);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
    }
}}