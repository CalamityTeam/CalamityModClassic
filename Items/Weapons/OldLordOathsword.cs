using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class OldLordOathsword : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/OldLordOathsword");
		return true;
	}
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Old Lord Oathsword");
		Item.damage = 40;
		Item.width = 70;
		Item.height = 70;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.useAnimation = 24;
		Item.useStyle = 1;
		Item.useTime = 24;
		Item.useTurn = true;
		Item.knockBack = 4.5f;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.maxStack = 1;
		////Tooltip.SetDefault("A relic of the ancient underworld");
		Item.value = 120000;
		Item.rare = 3;
	}
}}
