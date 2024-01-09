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
public class AstralHelm : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 500000;
        Item.rare = ItemRarityID.Lime;
        Item.defense = 13; //53
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("AstralBreastplate").Type && legs.type == Mod.Find<ModItem>("AstralLeggings").Type;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawShadow = true;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Massively increased movement speed as health decreases\n" +
        	"Whenever you crit an enemy fallen, hallowed, and astral stars will rain down";
        CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
        modPlayer.astralStarRain = true;
        if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.moveSpeed += 0.1f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.moveSpeed += 0.2f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.moveSpeed += 0.3f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.moveSpeed += 0.4f;
		}
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.dangerSense = true;
        player.GetDamage(DamageClass.Melee) += 0.09f;
        player.GetCritChance(DamageClass.Melee) += 9;
        player.GetDamage(DamageClass.Ranged) += 0.09f;
        player.GetCritChance(DamageClass.Ranged) += 9;
        player.GetDamage(DamageClass.Magic) += 0.09f;
        player.GetCritChance(DamageClass.Magic) += 9;
        player.GetDamage(DamageClass.Throwing) += 0.09f;
        player.GetCritChance(DamageClass.Throwing) += 9;
        player.GetDamage(DamageClass.Summon) += 0.09f;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AstralBar", 8);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}