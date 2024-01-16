using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class MandibleClaws : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/MandibleClaws");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Mandible Claws");
		Item.width = 22;  //The width of the .png file in pixels divided by 2.
		Item.damage = 14;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 6;
		Item.useStyle = 1;
		Item.useTime = 6;
		Item.useTurn = true;
		Item.knockBack = 3.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 18;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 10000;  //Value is calculated in copper coins.
		Item.rare = 1;  //Ranges from 1 to 11.
	}
}}
