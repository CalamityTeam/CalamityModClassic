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
    public class TarragonHelm : ModItem
{

    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Tarragon Helm");
        Item.width = 18;
        Item.height = 18;
        //AddTooltip("Provides multiple defensive stat boosts, immune to lava, immune to cursed inferno, fire, cursed, and chilled debuffs, and can move freely through liquids");
        //AddTooltip2("Helm of the disciple of ancients");
        Item.value = 550000;
        Item.rare = 8;
        Item.defense = 12;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("TarragonBreastplate").Type && legs.type == Mod.Find<ModItem>("TarragonLeggings").Type;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = ("Grants multiple defense boosts if health is low");
        player.onHitRegen = true;
        if(player.statLife <= (player.statLifeMax2 * 0.75f))
        {
    		player.statDefense += 10;
    		player.endurance += 0.05f;
        	if(player.statLife <= (player.statLifeMax2 * 0.5f))
        	{
        		player.statDefense += 10;
        		player.endurance += 0.05f;
        		player.AddBuff(BuffID.Heartreach, 2);
        		if(player.statLife <= (player.statLifeMax2 * 0.25f))
        		{
        			player.AddBuff(BuffID.Calm, 2);
        			player.endurance += 0.10f;
        			player.statDefense += 10;
        		}
        	}
        }
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.endurance += 0.05f;
        player.statLifeMax2 += 100;
    	player.lavaImmune = true;
    	player.ignoreWater = true;
    	player.buffImmune[BuffID.CursedInferno] = true;
    	player.buffImmune[BuffID.OnFire] = true;
    	player.buffImmune[BuffID.Cursed] = true;
    	player.buffImmune[BuffID.Chilled] = true;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawOutlines = true;
    	player.armorEffectDrawShadow = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "UeliaceBar", 7);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
    }
}}