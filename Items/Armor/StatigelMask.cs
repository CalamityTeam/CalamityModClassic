using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items.Armor;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Head)]
public class StatigelMask : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 200000;
        Item.rare = ItemRarityID.Pink;
        Item.defense = 5; //23
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("StatigelArmor").Type && legs.type == Mod.Find<ModItem>("StatigelGreaves").Type;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Grants throwing damage, critical strike chance, and movement speed as health gets lower";
        if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.GetCritChance(DamageClass.Throwing) += 3;
			player.GetDamage(DamageClass.Throwing) += 0.03f;
			player.moveSpeed += 0.025f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.GetCritChance(DamageClass.Throwing) += 6;
			player.GetDamage(DamageClass.Throwing) += 0.06f;
			player.moveSpeed += 0.05f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.GetCritChance(DamageClass.Throwing) += 9;
			player.GetDamage(DamageClass.Throwing) += 0.09f;
			player.moveSpeed += 0.1f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.GetCritChance(DamageClass.Throwing) += 15;
			player.GetDamage(DamageClass.Throwing) += 0.15f;
			player.moveSpeed += 0.15f;
		}
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.ThrownCost33 = true;
    	player.GetDamage(DamageClass.Throwing) += 0.1f;
        player.GetCritChance(DamageClass.Throwing) += 5;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "PurifiedGel", 5);
        recipe.AddIngredient(ItemID.HellstoneBar, 9);
		recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}