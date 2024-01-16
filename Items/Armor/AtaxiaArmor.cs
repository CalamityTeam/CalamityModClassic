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
    [AutoloadEquip(EquipType.Body)]
    public class AtaxiaArmor : ModItem
{

    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Ataxia Armor");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("+50 Max Mana\n8% increased magic damage and 5% increased magic critical strike chance");
        Item.value = 400000;
        Item.rare = 7;
        Item.defense = 18; //42
    }

    public override void UpdateEquip(Player player)
    {
        player.statManaMax2 += 50;
        player.GetDamage(DamageClass.Magic) += 0.08f;
        player.GetCritChance(DamageClass.Magic) += 5;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CruptixBar", 15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}