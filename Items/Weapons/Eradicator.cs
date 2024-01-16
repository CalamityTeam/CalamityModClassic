using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Eradicator : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/10.DevourerofGods/Eradicator");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Eradicator");
		Item.width = 24;  //The width of the .png file in pixels divided by 2.
		Item.damage = 250;  //Keep this reasonable please.
		Item.noMelee = true;  //Dictates whether this is a melee-class weapon.
		Item.noUseGraphic = true;
		Item.autoReuse = true;
		Item.useAnimation = 19;
		Item.useStyle = 1;
		Item.useTime = 19;
		////Tooltip.SetDefault("Annihilate!");
		Item.knockBack = 7f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.DamageType = DamageClass.Throwing;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 44;  //The height of the .png file in pixels divided by 2.
		Item.value = 1250000;  //Value is calculated in copper coins.
		Item.rare = 10;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("EradicatorProjectile").Type;
		Item.shootSpeed = 12f;
	}
}}
