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
public class ReaverHelm : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 350000;
        Item.rare = ItemRarityID.LightPurple;
        Item.defense = 25; //58
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("ReaverScaleMail").Type && legs.type == Mod.Find<ModItem>("ReaverCuisses").Type;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawShadowSubtle = true;
    	player.armorEffectDrawOutlines = true;
    }

    public override void UpdateArmorSet(Player player)
    {
    	CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		player.thorns = 0.5f;
    	modPlayer.reaverBlast = true;
        player.setBonus = "Grants increased melee and movement stats as health decreases\n" +
        	"You take slightly more damage as health decreases\n" +
        	"Projectiles explode on hit\n" +
        	"Reaver thorns\n" +
        	"Rage activates when you are damaged";
        if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.endurance -= 0.05f;
			player.moveSpeed += 0.05f;
			player.GetCritChance(DamageClass.Melee) += 5;
			player.GetDamage(DamageClass.Melee) += 0.05f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.endurance -= 0.1f;
			player.moveSpeed += 0.1f;
			player.GetCritChance(DamageClass.Melee) += 10;
			player.GetDamage(DamageClass.Melee) += 0.1f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.endurance -= 0.2f;
			player.moveSpeed += 0.2f;
			player.GetCritChance(DamageClass.Melee) += 15;
			player.GetDamage(DamageClass.Melee) += 0.15f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.endurance -= 0.3f;
			player.moveSpeed += 0.3f;
			player.GetCritChance(DamageClass.Melee) += 20;
			player.GetDamage(DamageClass.Melee) += 0.2f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.2f;
		}
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.lavaImmune = true;
    	player.ignoreWater = true;
    	player.buffImmune[BuffID.CursedInferno] = true;
    	player.GetDamage(DamageClass.Melee) += 0.1f;
        player.GetCritChance(DamageClass.Melee) += 5;
        player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
        player.moveSpeed += 0.05f;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "DraedonBar", 8);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}