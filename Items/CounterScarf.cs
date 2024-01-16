using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items {
public class CounterScarf : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Accessories/CounterScarf");
        return true;
    }
	
	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Counter Scarf");
		////Tooltip.SetDefault("Melee weapons that don't fire projectiles are granted 25% more damage\nGrants the ability to dash and dodge attacks\nAfter a dodge you will be granted a buff to melee damage, speed, and crit chance for a short time\nAfter a successful dodge you must wait 10 seconds before you can dodge again");
		Item.width = 26;
		Item.height = 26;
		Item.value = 50000;
		Item.rare = 5;
		Item.accessory = true;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
		modPlayer.dodgeScarf = true;
		modPlayer.dashMod = 1;
	}
}}