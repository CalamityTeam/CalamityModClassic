using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Plaguebringer {
public class VirulentKatana : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Virulence");
	}

	public override void SetDefaults()
	{
		Item.width = 42;  //The width of the .png file in pixels divided by 2.
		Item.damage = 83;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 15;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 15;
		Item.useTurn = true;
		Item.knockBack = 5.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 52;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 755000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Yellow;  //Ranges from 1 to 11.
		Item.shoot = Mod.Find<ModProjectile>("PlagueDust").Type;
		Item.shootSpeed = 9f;
	}
    
    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
    	target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 240);
	}
}}
