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
public class StatigelHelm : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 200000;
        Item.rare = ItemRarityID.Pink;
        Item.defense = 9; //27
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("StatigelArmor").Type && legs.type == Mod.Find<ModItem>("StatigelGreaves").Type;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Grants melee damage, critical strike chance, and speed as health gets lower";
        if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.GetCritChance(DamageClass.Melee) += 3;
			player.GetDamage(DamageClass.Melee) += 0.03f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.025f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.GetCritChance(DamageClass.Melee) += 6;
			player.GetDamage(DamageClass.Melee) += 0.06f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.GetCritChance(DamageClass.Melee) += 9;
			player.GetDamage(DamageClass.Melee) += 0.09f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.GetCritChance(DamageClass.Melee) += 15;
			player.GetDamage(DamageClass.Melee) += 0.15f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
		}
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.GetDamage(DamageClass.Melee) += 0.07f;
        player.GetCritChance(DamageClass.Melee) += 5;
        player.GetAttackSpeed(DamageClass.Melee) += 0.07f;
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