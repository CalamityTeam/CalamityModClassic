using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Leviathan {
public class EnchantedPearl : ModItem
{
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Enchanted Pearl");
		////Tooltip.SetDefault("Increases fishing skill\nCrate potion effect, does not stack with crate potions");
		Item.width = 26;
		Item.height = 26;
		Item.value = 150000;
		Item.rare = 7;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.fishingSkill += 10;
		player.cratePotion = true;
	}
}}