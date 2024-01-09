using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items.Armor;

namespace CalamityModClassic1Point2.Items.Armor 
{
	[AutoloadEquip(EquipType.Body)]
	public class DemonshadeBreastplate : ModItem
	{
	    public override void SetDefaults()
	    {
	        Item.width = 18;
	        Item.height = 18;
	        Item.value = 10000000;
	        Item.defense = 62;
	    }
	    
	    public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(255, 0, 255);
	            }
	        }
	    }
	
	    public override void UpdateEquip(Player player)
	    {
	    	CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
	    	modPlayer.shadeRegen = true;
	    	player.thorns = 100f;
	    	player.statLifeMax2 += 1000;
	        player.statManaMax2 += 500;
	        player.GetCritChance(DamageClass.Magic) += 20;
			player.GetDamage(DamageClass.Magic) += 0.2f;
			player.GetCritChance(DamageClass.Melee) += 20;
			player.GetDamage(DamageClass.Melee) += 0.2f;
			player.GetCritChance(DamageClass.Throwing) += 20;
			player.GetDamage(DamageClass.Throwing) += 0.2f;
			player.GetCritChance(DamageClass.Ranged) += 20;
			player.GetDamage(DamageClass.Ranged) += 0.2f;
			player.GetDamage(DamageClass.Summon) += 0.2f;
			player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "ShadowspecBar", 50);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	}
}