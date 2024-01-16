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
    public class AerospecHelm : ModItem
{

    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Aerospec Helm");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("8% increase to all damage types");
        Item.value = 50000;
        Item.rare = 3;
        Item.defense = 4; //15
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == Mod.Find<ModItem>("AerospecBreastplate").Type && legs.type == Mod.Find<ModItem>("AerospecLeggings").Type;
    }
    
    public override void ArmorSetShadows(Player player)
    {
    	player.armorEffectDrawShadow = true;
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus =("Increased movement speed as health decreases");
        if(player.statLife <= (player.statLifeMax2 * 0.75f))
        {
        	player.moveSpeed += 0.05f;
        	if(player.statLife <= (player.statLifeMax2 * 0.5f))
        	{
        		player.moveSpeed += 0.1f;
        		if(player.statLife <= (player.statLifeMax2 * 0.25f))
        		{
        			player.moveSpeed += 0.15f;
        		}
        	}
        }
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.GetDamage(DamageClass.Melee) *= 1.08f;
        player.GetDamage(DamageClass.Throwing) *= 1.08f;
        player.GetDamage(DamageClass.Ranged) *= 1.08f;
        player.GetDamage(DamageClass.Magic) *= 1.08f;
        player.GetDamage(DamageClass.Summon) *= 1.08f;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AerialiteBar", 5);
        recipe.AddIngredient(ItemID.Cloud, 3);
        recipe.AddIngredient(ItemID.RainCloud, 1);
        recipe.AddIngredient(ItemID.Feather, 1);
        recipe.AddTile(TileID.SkyMill);
        recipe.Register();
    }
}}