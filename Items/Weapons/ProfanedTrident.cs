using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class ProfanedTrident : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/11.Yharon/ProfanedTrident");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Profaned Trident");
		Item.width = 72;  //The width of the .png file in pixels divided by 2.
		Item.damage = 400;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 16;
		Item.useStyle = 1;
		Item.useTime = 16;
		Item.knockBack = 8f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 72;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 10000000;  //Value is calculated in copper coins.
		Item.rare = 10;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("ProfanedTrident").Type;
		Item.shootSpeed = 24f;
	}
}}
