using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Cryogen {
public class CryoBar : ModItem
{
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Cryo Bar");
		Item.width = 15;
		Item.height = 12;
		////Tooltip.SetDefault("Cold to the touch");
		Item.maxStack = 999;
		Item.value = 28750;
		Item.rare = 5;
	}
}}