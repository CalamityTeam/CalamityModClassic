using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items.Armor;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.Armor 
{
	[AutoloadEquip(EquipType.Body)]
	public class DemonshadeBreastplate : ModItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Demonshade Breastplate");
            /* Tooltip.SetDefault("20% increased melee speed, 15% increased damage and critical strike chance\n" +
                       "Enemies take ungodly damage when they touch you\n" +
                       "Increased max life and mana by 200\n" +
                       "Standing still lets you absorb the shadows and boost your life regen"); */
        }

	    public override void SetDefaults()
	    {
	        Item.width = 18;
	        Item.height = 18;
			Item.value = Item.buyPrice(4, 0, 0, 0);
			Item.defense = 50;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 16;
		}
	
	    public override void UpdateEquip(Player player)
	    {
	    	CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
	    	modPlayer.shadeRegen = true;
	    	player.thorns = 100f;
	    	player.statLifeMax2 += 200;
	        player.statManaMax2 += 200;
	        player.GetCritChance(DamageClass.Magic) += 15;
			player.GetDamage(DamageClass.Magic) += 0.15f;
			player.GetCritChance(DamageClass.Melee) += 15;
			player.GetDamage(DamageClass.Melee) += 0.15f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 15;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.15f;
			player.GetCritChance(DamageClass.Ranged) += 15;
			player.GetDamage(DamageClass.Ranged) += 0.15f;
			player.GetDamage(DamageClass.Summon) += 0.15f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.2f;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "ShadowspecBar", 15);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	}
}