using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Cryogen {
public class CryoBar : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Cryo Bar");
		//Tooltip.SetDefault("Cold to the touch");
	}
	
	public override void SetDefaults()
	{
		Item.width = 15;
		Item.height = 12;
		Item.maxStack = 999;
		Item.value = 28750;
		Item.rare = ItemRarityID.Pink;
	}
}}