using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons.Polterghast
{
	public class TerrorBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Terror Blade");
			// Tooltip.SetDefault("Fires a terror beam that bounces off tiles\nOn every bounce it emits an explosion");
		}

		public override void SetDefaults()
		{
			Item.width = 88;
			Item.damage = 250;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 18;
			Item.useTime = 18;
			Item.useTurn = true;
			Item.useStyle = 1;
			Item.knockBack = 8.5f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 80;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("TerrorBeam").Type;
			Item.shootSpeed = 20f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}
	
	    public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(3) == 0)
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 60);
	        }
	    }
	}
}
