using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Calamitas {
public class CrushsawCrasher : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Crushsaw Crasher");
	}

	public override void SetDefaults()
	{
		Item.width = 38;  //The width of the .png file in pixels divided by 2.
		Item.damage = 65;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Throwing;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 18;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 18;
		Item.knockBack = 7f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 22;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 500000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.LightPurple;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("Crushax").Type;
		Item.shootSpeed = 11f;
	}
	
	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
		target.AddBuff(Mod.Find<ModBuff>("BrimstoneFlames").Type, 300);
	}
}}
