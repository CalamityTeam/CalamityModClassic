using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class MantisClaws : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mantis Claws");
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.damage = 120;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 6;
			Item.useStyle = 1;
			Item.useTime = 6;
			Item.useTurn = true;
			Item.knockBack = 7f;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.height = 20;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(4) == 0)
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 33);
	        }
	    }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, 0f, 0f, 612, 0, Item.knockBack, Main.myPlayer);
        }
    }
}
