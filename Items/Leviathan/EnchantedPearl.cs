using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Leviathan {
public class EnchantedPearl : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Enchanted Pearl");
		//Tooltip.SetDefault("Increases fishing skill\nCrate potion effect, does not stack with crate potions");
	}
	
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 26;
		Item.value = 150000;
		Item.rare = ItemRarityID.Lime;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.fishingSkill += 10;
		player.cratePotion = true;
	}
}}