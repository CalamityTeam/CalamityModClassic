using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.SlimeGod {
public class ManaOverloader : ModItem
{
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Mana Overloader");
		////Tooltip.SetDefault("Increases max mana by 50 and magic damage by 15%\nLife regen lowered by 5 if mana is above 50% of its maximum\nMagic healing if mana is below 25% of its maximum");
		Item.width = 30;
		Item.height = 30;
		Item.value = 15000;
		Item.rare = 5;
		Item.accessory = true;
		Item.expert = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.statManaMax2 += 50;
		player.GetDamage(DamageClass.Magic) += 0.15f;
		if(player.statMana >= (player.statManaMax2 * 0.5f))
		{
			player.lifeRegen -= 5;
		}
		if (player.statMana < (player.statManaMax2 * 0.25f))
		{
			player.ghostHeal = true;
		}
	}
}}