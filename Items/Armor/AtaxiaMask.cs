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
    [AutoloadEquip(EquipType.Head)]
    public class AtaxiaMask : ModItem
{

    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Ataxia Mask");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("15% increased magic damage, reduces mana usage by 15%, and 10% increased magic critical strike chance\n+150 Max Mana\nImmune to lava and fire damage");
        Item.value = 450000;
        Item.rare = 7;
        Item.defense = 11; //42
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
        player.setBonus =("Major magic damage buffs and defense debuffs as health decreases\nInferno effect when below 50% life\nMagic attacks summon damaging and healing flare orbs on hit");
        CalamityPlayer1Point1.ataxiaHurt = true;
        CalamityPlayer1Point1.ataxiaHeal = true;
        if(player.statLife <= (player.statLifeMax2 * 0.75f))
        {
        	player.manaCost *= 1.2f;
        	player.GetCritChance(DamageClass.Magic) += 15;
        	if(player.statLife <= (player.statLifeMax2 * 0.5f))
        	{
        		player.manaCost *= 1.2f;
        		player.AddBuff(BuffID.Inferno, 2);
        		if(player.statLife <= (player.statLifeMax2 * 0.25f))
        		{
        			player.manaCost *= 1.2f;
        			player.endurance -= 0.5f;
        			player.GetDamage(DamageClass.Magic) += 0.5f;
        		}
        	}
        }
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.manaCost *= 0.85f;
        player.statManaMax2 += 150;
        player.GetDamage(DamageClass.Magic) += 0.15f;
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