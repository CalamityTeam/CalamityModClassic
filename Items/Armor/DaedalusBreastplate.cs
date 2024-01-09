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
    public class DaedalusBreastplate : ModItem
{

    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Daedalus Breastplate");
        Item.width = 18;
        Item.height = 18;
        //AddTooltip("5% increased ranged damage, 20% chance to not consume ammo, and 15% increased ranged critical strike chance");
        Item.value = 250000;
        Item.rare = 5;
        Item.defense = 16;
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Ranged) += 0.05f;
        player.GetCritChance(DamageClass.Ranged) += 15;
        player.ammoCost80 = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VerstaltiteBar", 15);
		recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}