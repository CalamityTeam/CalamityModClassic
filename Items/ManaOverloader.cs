using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items {
public class ManaOverloader : ModItem
{	
	public override void SetDefaults()
	{
		//DisplayName.SetDefault("Mana Overloader");
		//AddTooltip("Increases max mana by 100 and magic damage by 25%");
		//AddTooltip2("Health regen becomes 0 if mana is above 50%");
		Item.width = 30;
		Item.height = 30;
		Item.value = 15000;
		Item.rare = 5;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.statManaMax2 += 100;
		player.GetDamage(DamageClass.Magic) += 0.25f;
		if(player.statMana >= (player.statManaMax2 * 0.5f))
		{
			if(player.lifeRegen > 0)
			{
				player.lifeRegen = 0;
			}
		}
	}
}}