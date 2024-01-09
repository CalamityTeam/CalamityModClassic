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
    [AutoloadEquip(EquipType.Head)]
    public class DaedalusHelm : ModItem
{

    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Daedalus Helm");
        Item.width = 18;
        Item.height = 18;
        //AddTooltip("20% increased ranged damage and 15% increased ranged critical strike chance");
        //AddTooltip2("Immune to Cursed, Cursed Inferno, and gives control over gravity");
        Item.value = 300000;
        Item.rare = 5;
        Item.defense = 13;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("DaedalusBreastplate").Type && legs.type == Mod.Find<ModItem>("DaedalusLeggings").Type;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = ("Grants ranged damage, ranged crit, and defensive boosts as health gets lower");
        player.shroomiteStealth = true;
        if(player.statLife <= (player.statLifeMax2 * 0.75f))
        {
        	player.GetCritChance(DamageClass.Ranged) += 5;
        	player.GetDamage(DamageClass.Ranged) += 0.10f;
        	if(player.statLife <= (player.statLifeMax2 * 0.5f))
        	{
        		player.GetCritChance(DamageClass.Ranged) += 10;
        		if(player.statLife <= (player.statLifeMax2 * 0.25f))
        		{
        			player.GetCritChance(DamageClass.Ranged) += 10;
        			player.endurance += 0.15f;
        			player.GetDamage(DamageClass.Ranged) += 0.15f;
        		}
        	}
        }
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.GetDamage(DamageClass.Ranged) += 0.20f;
        player.GetCritChance(DamageClass.Ranged) += 15;
        player.AddBuff(BuffID.Gravitation, 2);
    	player.buffImmune[BuffID.Cursed] = true;
    	player.buffImmune[BuffID.CursedInferno] = true;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawOutlines = true;
    	player.armorEffectDrawShadow = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "VerstaltiteBar", 8);
		recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}