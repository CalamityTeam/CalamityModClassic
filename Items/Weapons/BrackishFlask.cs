using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class BrackishFlask : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/7.Leviathan/BrackishFlask");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Brackish Flask");
		Item.width = 28;  //The width of the .png file in pixels divided by 2.
		Item.damage = 50;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.noUseGraphic = true;
		////Tooltip.SetDefault("Full of poisonous saltwater");
		Item.useAnimation = 35;
		Item.useStyle = 1;
		Item.useTime = 35;
		Item.knockBack = 6.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item106;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 30;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 600000;  //Value is calculated in copper coins.
		Item.rare = 7;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("BrackishFlask").Type;
		Item.shootSpeed = 12f;
	}
}}
