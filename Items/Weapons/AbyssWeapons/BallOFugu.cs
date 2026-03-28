using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.AbyssWeapons
{
	public class BallOFugu : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ball O' Fugu");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 40;
	        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
	        Item.width = 30;
	        Item.height = 10;
	        Item.useTime = 20;
	        Item.useAnimation = 20;
	        Item.useStyle = 5;
	        Item.noMelee = true;
            Item.noUseGraphic = true;
	        Item.knockBack = 8f;
            Item.value = Item.buyPrice(0, 4, 0, 0);
            Item.rare = 3;
	        Item.UseSound = SoundID.Item1;
	        Item.autoReuse = true;
            Item.channel = true;
	        Item.shoot = Mod.Find<ModProjectile>("BallOFugu").Type;
	        Item.shootSpeed = 12f;
	    }
	}
}