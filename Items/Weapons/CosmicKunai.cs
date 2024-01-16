using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class CosmicKunai : ModItem
{
    public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/CosmicKunai");
        return true;
    }

	public override void SetDefaults()
	{
		//Tooltip.SetDefault("Cosmic Kunai");
		Item.width = 26;  //The width of the .png file in pixels divided by 2.
		Item.damage = 105;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.noUseGraphic = true;
		////Tooltip.SetDefault("Fires a stream of short-range kunai");
		Item.useTime = 1;
		Item.useAnimation = 5;
		Item.useStyle = 1;
		Item.knockBack = 5.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item109;
		Item.autoReuse = true;  //Dictates whether the weapon can be("auto-fired".
		Item.height = 48;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 1500000;  //Value is calculated in copper coins.
		Item.rare = 10;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("CosmicKunai").Type;
		Item.shootSpeed = 28f;
	}
}}
