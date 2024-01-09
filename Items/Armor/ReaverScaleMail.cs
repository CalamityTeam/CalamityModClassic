using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items.Armor;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Body)]
public class ReaverScaleMail : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Reaver Scale Mail");
        //Tooltip.SetDefault("5% increased damage and crit chance\nProvides life regeneration\n+50 max life");
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.lifeRegen = 2;
        Item.value = 300000;
        Item.rare = ItemRarityID.LightPurple;
        Item.defense = 19;
    }

    public override void UpdateEquip(Player player)
    {
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
        recipe.AddIngredient(null, "DraedonBar", 15);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}