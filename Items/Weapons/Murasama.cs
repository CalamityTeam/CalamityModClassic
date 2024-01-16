using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Murasama : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/Murasama");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Murasama");
		Item.width = 72;  //The width of the .png file in pixels divided by 2.
		Item.damage = 666;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.noUseGraphic = true;
		Item.channel = true;
		Item.useAnimation = 25;
		Item.useStyle = 5;
		Item.useTime = 5;
		////Tooltip.SetDefault("There will be blood!");
		Item.knockBack = 6.5f;  //Ranges from 1 to 9.
		Item.autoReuse = false;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 78;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 30000000;  //Value is calculated in copper coins.
		Item.rare = 10;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("Murasama").Type;
		Item.shootSpeed = 15f;
	}
}}
