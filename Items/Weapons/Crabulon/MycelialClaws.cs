using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Crabulon
{
	public class MycelialClaws : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Mycelial Claws");
		}

		public override void SetDefaults()
		{
			Item.width = 22;  //The width of the .png file in pixels divided by 2.
			Item.damage = 14;  //Keep this reasonable please.
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
			Item.useAnimation = 6;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 6;
			Item.useTurn = true;
			Item.knockBack = 3.75f;  //Ranges from 1 to 9.
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
			Item.height = 24;  //The height of the .png file in pixels divided by 2.
			Item.maxStack = 1;
			Item.value = 40000;  //Value is calculated in copper coins.
			Item.rare = ItemRarityID.Green;  //Ranges from 1 to 11.
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.NextBool(4))
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.BlueFairy);
	        }
	    }
	}
}
