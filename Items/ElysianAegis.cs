using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
	[AutoloadEquip(EquipType.Shield)]
public class ElysianAegis : ModItem
{	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Elysian Aegis");
		////Tooltip.SetDefault("Grants immunity to fire blocks and knockback\n+100 max life and increased life regen\nGrants a supreme holy flame dash\nCan be used to ram enemies\nTap down to activate buffs to all damage, crit chance, and defense\nActivating this buff will reduce your movement speed and increase enemy aggro");
		Item.width = 48;
		Item.height = 42;
		Item.value = 10000000;
		Item.expert = true;
		Item.defense = 10;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		modPlayer.dashMod = 3;
		modPlayer.elysianAegis = true;
		player.noKnockback = true;
		player.fireWalk = true;
		player.lifeRegen += 3;
		player.statLifeMax2 += 100;
	}
}}