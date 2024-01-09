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
public class AerospecHat : ModItem
{
    public override void SetStaticDefaults()
    {
        //DisplayName.SetDefault("Aerospec Hat");
        //Tooltip.SetDefault("8% increased magic damage");
    }

    public override void SetDefaults()
    {
        Item.width = 18;
        Item.height = 18;
        Item.value = 50000;
        Item.rare = ItemRarityID.Orange;
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
        player.setBonus = "Increased movement speed and magic critical strike chance as health decreases";
        if(player.statLife <= (player.statLifeMax2 * 0.8f) && player.statLife > (player.statLifeMax2 * 0.6f))
		{
			player.moveSpeed += 0.05f;
			player.GetCritChance(DamageClass.Magic) += 5;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.6f) && player.statLife > (player.statLifeMax2 * 0.4f))
		{
			player.moveSpeed += 0.1f;
			player.GetCritChance(DamageClass.Magic) += 10;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.4f) && player.statLife > (player.statLifeMax2 * 0.2f))
		{
			player.moveSpeed += 0.15f;
			player.GetCritChance(DamageClass.Magic) += 15;
		}
		else if(player.statLife <= (player.statLifeMax2 * 0.2f))
		{
			player.moveSpeed += 0.2f;
			player.GetCritChance(DamageClass.Magic) += 20;
		}
    }
    
    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Magic) += 0.08f;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "AerialiteBar", 5);
        recipe.AddIngredient(ItemID.Cloud, 3);
        recipe.AddIngredient(ItemID.RainCloud);
        recipe.AddIngredient(ItemID.Feather);
        recipe.AddTile(TileID.SkyMill);
        recipe.Register();
    }
}}