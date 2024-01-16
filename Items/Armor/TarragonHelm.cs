using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items.Armor;

namespace CalamityModClassic1Point1.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class TarragonHelm : ModItem
{
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Tarragon Helm");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("+100 max life\nImmune to lava, cursed inferno, fire, cursed, and chilled debuffs\nCan move freely through liquids\nHelm of the disciple of ancients");
        Item.value = 550000;
        Item.rare = 8;
        Item.defense = 12; //50
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("TarragonBreastplate").Type && legs.type == Mod.Find<ModItem>("TarragonLeggings").Type;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawShadowSubtle = true;
    	player.armorEffectDrawOutlines = true;
    }

    public override void UpdateArmorSet(Player player)
    {
    	CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
    	modPlayer.tarraRegen = true;
    	modPlayer.tarraReach = true;
    	modPlayer.tarraCalm = true;
        player.setBonus =("Grants multiple defense boosts if health is low\nYou regen health quickly when you damage enemies\nEnemies are less likely to target you\nIncreased heart pickup range");
        if(player.statLife <= (player.statLifeMax2 * 0.75f))
        {
    		player.statDefense += 7;
    		player.endurance += 0.025f;
        	if(player.statLife <= (player.statLifeMax2 * 0.5f))
        	{
        		player.statDefense += 7;
        		player.endurance += 0.05f;
        		if(player.statLife <= (player.statLifeMax2 * 0.25f))
        		{
        			player.endurance += 0.075f;
        			player.statDefense += 7;
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

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "UeliaceBar", 7);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}