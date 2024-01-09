using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class TeardropCleaver : ModItem
{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Teardrop Cleaver");
			//Tooltip.SetDefault("Makes your enemies cry");
		}

	public override void SetDefaults()
	{
		Item.width = 52;  //The width of the .png file in pixels divided by 2.
		Item.damage = 15;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.useAnimation = 24;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useTime = 24;
		Item.useTurn = true;
		Item.knockBack = 4.5f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.height = 56;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 50000;  //Value is calculated in copper coins.
		Item.rare = ItemRarityID.Green;  //Ranges from 1 to 11.
	}

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
    	target.AddBuff(Mod.Find<ModBuff>("TemporalSadness").Type, 60);
	}
}}
