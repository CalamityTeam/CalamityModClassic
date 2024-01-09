using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Body)]
public class AtaxiaArmor : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Ataxia Armor");
        //Tooltip.SetDefault("+50 max mana and max life\n5% increased damage and critical strike chance");
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 400000;
        Item.rare = ItemRarityID.Yellow;
        Item.defense = 21;
    }

    public override void UpdateEquip(Player player)
    {
        player.statManaMax2 += 50;
        player.statLifeMax2 += 50;
        player.GetCritChance(DamageClass.Melee) += 5;
		player.GetDamage(DamageClass.Melee) += 0.05f;
		player.GetCritChance(DamageClass.Magic) += 5;
		player.GetDamage(DamageClass.Magic) += 0.05f;
		player.GetCritChance(DamageClass.Ranged) += 5;
		player.GetDamage(DamageClass.Ranged) += 0.05f;
		player.GetCritChance(DamageClass.Throwing) += 5;
		player.GetDamage(DamageClass.Throwing) += 0.05f;
		player.GetDamage(DamageClass.Summon) += 0.05f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CruptixBar", 15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}