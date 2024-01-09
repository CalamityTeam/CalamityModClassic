using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Armor {
[AutoloadEquip(EquipType.Head)]
public class AtaxiaMask : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 450000;
        Item.rare = ItemRarityID.Yellow;
        Item.defense = 9; //45
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("AtaxiaArmor").Type && legs.type == Mod.Find<ModItem>("AtaxiaSubligar").Type;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawOutlines = true;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Magic damage buffs and slight defense debuffs as health decreases\n" +
        	"Inferno effect when below 50% life\n" +
        	"Magic attacks summon damaging and healing flare orbs on hit\n" +
        	"You have a 20% chance to emit a blazing explosion when you are hit";
        CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		modPlayer.ataxiaBlaze = true;
        modPlayer.ataxiaMage = true;
        if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.endurance -= 0.025f;
			player.GetCritChance(DamageClass.Magic) += 5;
			player.GetDamage(DamageClass.Magic) += 0.05f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.endurance -= 0.05f;
			player.GetCritChance(DamageClass.Magic) += 10;
			player.GetDamage(DamageClass.Magic) += 0.1f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.endurance -= 0.1f;
			player.GetCritChance(DamageClass.Magic) += 15;
			player.GetDamage(DamageClass.Magic) += 0.15f;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.endurance -= 0.15f;
			player.GetCritChance(DamageClass.Magic) += 20;
			player.GetDamage(DamageClass.Magic) += 0.2f;
		}
        if(player.statLife <= (player.statLifeMax2 * 0.5f))
       	{
       		player.AddBuff(BuffID.Inferno, 2);
       	}
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.manaCost *= 0.9f;
        player.statManaMax2 += 100;
        player.GetDamage(DamageClass.Magic) += 0.12f;
        player.GetCritChance(DamageClass.Magic) += 10;
    	player.lavaImmune = true;
    	player.buffImmune[BuffID.OnFire] = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CruptixBar", 7);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}