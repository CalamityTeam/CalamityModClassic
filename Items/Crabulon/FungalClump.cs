using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Crabulon 
{
	public class FungalClump : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fungal Clump");
			/* Tooltip.SetDefault("Summons a fungal clump to fight for you\n" +
	                   "The clump latches onto enemies and steals their life for you"); */
		}
		
	    public override void SetDefaults()
	    {
	        Item.width = 20;
	        Item.height = 26;
            Item.value = Item.buyPrice(0, 9, 0, 0);
            Item.expert = true;
	        Item.accessory = true;
	    }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            if (modPlayer.fungalClump)
            {
                return false;
            }
            return true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
	    	CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.fungalClump = true;
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.FindBuffIndex(Mod.Find<ModBuff>("FungalClump").Type) == -1)
				{
					player.AddBuff(Mod.Find<ModBuff>("FungalClump").Type, 3600, true);
				}
				if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("FungalClump").Type] < 1)
				{
					Projectile.NewProjectile(Entity.GetSource_FromThis(null), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("FungalClump").Type, (int)(10f * player.GetDamage(DamageClass.Summon).Multiplicative), 1f, Main.myPlayer, 0f, 0f);
				}
			}
		}
	}
}