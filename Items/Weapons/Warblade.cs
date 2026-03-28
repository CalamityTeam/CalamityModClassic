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
    public class Warblade : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Warblade");
			// Tooltip.SetDefault("Critical hits cleave enemy armor, reducing their defense by 15 and protection by 25%");
		}

        public override void SetDefaults()
        {
            Item.damage = 27;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 32;
            Item.height = 48;
            Item.useTime = 19;
            Item.useAnimation = 19;
            Item.useTurn = true;
            Item.useStyle = 1;
            Item.knockBack = 4.25f;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 1;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			if (hit.Crit)
			{
				target.AddBuff(Mod.Find<ModBuff>("WarCleave").Type, 450);
			}
		}
	}
}