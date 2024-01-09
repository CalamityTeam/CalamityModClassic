using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.BrimstoneWaifu {
public class Brimlance : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Brimlance");
		//Tooltip.SetDefault("This spear causes brimstone explosions on enemy hits\nEnemies killed by the spear drop brimstone fire");
	}

	public override void SetDefaults()
	{
		Item.width = 56;  //The width of the .png file in pixels divided by 2.
		Item.damage = 75;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.useTurn = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 19;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.useTime = 19;
		Item.knockBack = 7.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = false;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 56;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 200000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Pink;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("Brimlance").Type;
		Item.shootSpeed = 12f;
	}
}}
